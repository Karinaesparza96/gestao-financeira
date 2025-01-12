namespace Api.Dtos;

public class ResponseDefaultSucesso
{
    public bool Success { get; set; } = true;
    public object? Data { get; set; }
    public IEnumerable<string>? Avisos { get; set; }
}

public class ResponseDefaultFalha
{
    public bool Success { get; set; }
    public IEnumerable<string>? Erros { get; set; }
}