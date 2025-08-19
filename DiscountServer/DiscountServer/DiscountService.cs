using System.Security.Cryptography;
using System.Text;

public class DiscountService
{
    private readonly DiscountStorage _storage;
    private readonly char[] _charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

    public DiscountService(DiscountStorage storage)
    {
        _storage = storage;
    }

    public List<string> GenerateCodes(int count, int length)
    {
        var codes = new List<string>();
        var rng = RandomNumberGenerator.Create();

        while (codes.Count < count)
        {
            var code = GenerateCode(length, rng);
            if (_storage.AddCode(code))
                codes.Add(code);
        }

        return codes;
    }

    private string GenerateCode(int length, RandomNumberGenerator rng)
    {
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        var sb = new StringBuilder(length);
        foreach (var b in bytes)
            sb.Append(_charset[b % _charset.Length]);
        return sb.ToString();
    }

    public byte UseCode(string code)
    {
        if (!_storage.GetAllCodes().Contains(code))
            return 1;
        if (_storage.IsUsed(code))
            return 2;
        return _storage.UseCode(code) ? (byte)0 : (byte)3;
    }
}
