public class GenerateRequest
{
    public ushort Count { get; set; }
    public byte Length { get; set; }
}

public class GenerateResponse
{
    public bool Result { get; set; }
    public List<string> Codes { get; set; } = new();
}

public class UseCodeRequest
{
    public string Code { get; set; } = string.Empty;
}

public class UseCodeResponse
{
    public byte Result { get; set; }
}
