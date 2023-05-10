namespace Classify.Service.Interfaces;

public interface IAuthService
{
    ValueTask<string> GenerateToken(string email, string password);
}
