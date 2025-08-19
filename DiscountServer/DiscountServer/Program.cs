// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

var storage = new DiscountStorage();
var service = new DiscountService(storage);
var listener = new TcpListener(IPAddress.Any, 5000);
listener.Start();

Console.WriteLine("Server started on port 5000...");

while (true)
{
    var client = await listener.AcceptTcpClientAsync();
    _ = Task.Run(async () =>
    {
        using var stream = client.GetStream();
        using var reader = new StreamReader(stream);
        using var writer = new StreamWriter(stream) { AutoFlush = true };

        var request = await reader.ReadLineAsync();
        if (request == null) return;

        if (request.StartsWith("GEN"))
        {
            var json = request[3..];
            var req = JsonSerializer.Deserialize<GenerateRequest>(json);
            var codes = service.GenerateCodes(req.Count, req.Length);
            var res = new GenerateResponse { Result = true, Codes = codes };
            await writer.WriteLineAsync("RES" + JsonSerializer.Serialize(res));
            writer.Flush();
        }
        else if (request.StartsWith("USE"))
        {
            var json = request[3..];
            var req = JsonSerializer.Deserialize<UseCodeRequest>(json);
            var result = service.UseCode(req.Code);
            var res = new UseCodeResponse { Result = result };
            await writer.WriteLineAsync("RES" + JsonSerializer.Serialize(res));
            writer.Flush();
        }
    });
}
