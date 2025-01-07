namespace Api.Dtos;

public class ApiResponse
{
    public object? Data { get; set; }
    public IEnumerable<string>? Avisos { get; set; }
    public IEnumerable<string>? Erros { get; set; }
}