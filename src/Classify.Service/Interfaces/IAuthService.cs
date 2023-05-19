using Classify.Service.DTOs.LoginDto;

namespace Classify.Service.Interfaces;

public interface IAuthService
{
    Task<LoginResultDto> AuthenticateAsync(string email, string password);
}
