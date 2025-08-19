using System.Collections.Concurrent;

public class DiscountStorage
{
    private readonly string _filePath = "discount_codes.txt";
    private readonly ConcurrentDictionary<string, bool> _codes = new();

    public DiscountStorage()
    {
        if (File.Exists(_filePath))
        {
            foreach (var line in File.ReadAllLines(_filePath))
                _codes.TryAdd(line.Trim(), false);
        }
    }

    public IEnumerable<string> GetAllCodes() => _codes.Keys;

    public bool AddCode(string code)
    {
        if (_codes.TryAdd(code, false))
        {
            File.AppendAllLines(_filePath, new[] { code });
            return true;
        }
        return false;
    }

    public bool UseCode(string code)
    {
        return _codes.TryUpdate(code, true, false);
    }

    public bool IsUsed(string code)
    {
        return _codes.TryGetValue(code, out var used) && used;
    }
}
