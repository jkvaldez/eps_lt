using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Connecting to Discount Server...");
using var client = new TcpClient("localhost", 5000);
using var stream = client.GetStream();
using var writer = new StreamWriter(stream) { AutoFlush = true };
using var reader = new StreamReader(stream);

Console.Write("Welcome to Discount Mall! ");
Console.Write("\n\n\n ");
Console.Write("Please Choose a task Below: ");
Console.Write("\n\n\n ");
Console.Write("1: Generate Discount Code");
Console.Write("\n ");
Console.Write("2. Use Discount Code \n");

int number;
bool isValidInput = false;
do
{
    Console.Write("Please enter a Number: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out number))
    {
        if (number == 1 || number == 2)
        {
            isValidInput = true;
        }
        else
        {
            Console.WriteLine("Invalid input. The number must be 1 or 2.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid integer.");
    }

} while (!isValidInput);

int discountNumber;
bool isValidDiscountInput = false;
int lengthNumber;
bool isValidLengthNumber = false;
var genResponse = new GenerateResponse { Codes = new List<string>() };
if (number == 1)
{
    
    do
    {
        Console.Write("\n Please enter a number between 1 and 2000 for the Discount Count to be generated: ");
        string inputDiscount = Console.ReadLine();

        // Attempt to parse the input string to an integer
        if (int.TryParse(inputDiscount, out discountNumber))
        {
            // Check if the parsed number is within the desired range
            if (discountNumber >= 1 && discountNumber <= 2000)
            {
                isValidDiscountInput = true; // Input is valid, exit the loop
            }
            else
            {
                Console.WriteLine("Error: The number must be between 1 and 2000.");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
        }

    } while (!isValidDiscountInput);

    
    do
    {
        Console.Write("\n Please enter a number between 7 and 8 for the Discount Lenght to be generated: ");
        string inputLength = Console.ReadLine();

        // Attempt to parse the input string to an integer
        if (int.TryParse(inputLength, out lengthNumber))
        {
            // Check if the parsed number is within the desired range
            if (lengthNumber >= 7 && lengthNumber <= 8)
            {
                isValidLengthNumber = true; // Input is valid, exit the loop
            }
            else
            {
                Console.WriteLine("Error: The number must be between 7 and 8.");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid input. Please enter a valid integer.");
        }

    } while (!isValidLengthNumber);




    // Send GenerateRequest
    var genRequest = new GenerateRequest { Count = (ushort)discountNumber, Length = (byte)lengthNumber };
    var genJson = JsonSerializer.Serialize(genRequest);
    await writer.WriteLineAsync("GEN" + genJson);

    // Read GenerateResponse
    var genResponseLine = await reader.ReadLineAsync();
    if (genResponseLine != null && genResponseLine.StartsWith("RES"))
    {
        genResponse = JsonSerializer.Deserialize<GenerateResponse>(genResponseLine[3..]);
        Console.WriteLine("Generated Codes:");
        foreach (var code in genResponse.Codes)
            Console.WriteLine($" - {code}");

        
    }

}

if (number == 2)
{
    using var codeReader = new StreamReader(stream);
    Console.Write("\n Please enter a Discount Code to be Used: ");
    string inputDiscountCode = Console.ReadLine();

    // Test UseCodeRequest with first code
    var useRequest = new UseCodeRequest { Code = inputDiscountCode};
    var useJson = JsonSerializer.Serialize(useRequest);
    await writer.WriteLineAsync("USE" + useJson);

    var useResponseLine = await codeReader.ReadLineAsync();
    if (useResponseLine != null && useResponseLine.StartsWith("RES"))
    {
        var useResponse = JsonSerializer.Deserialize<UseCodeResponse>(useResponseLine[3..]);
        if (useResponse != null)
        {
            if (useResponse.Result is 1 or 2)
            {
                Console.WriteLine("\n Please Use a Valid Code");
            }
            else
            {
                Console.WriteLine("\n Code is Valid. Discount Code Success!");
            }
            
        }
    }

}
Console.ReadLine();