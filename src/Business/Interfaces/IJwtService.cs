namespace Business.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(string email);
    }
}
