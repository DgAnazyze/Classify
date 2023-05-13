using Classify.Service.DTOs.LoginDto;

namespace Classify.Service.Interfaces;

public interface IAuthService
{
    public Task<LoginResultDto> AuthenticateAsync(string email, string password);
}
