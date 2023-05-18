using Classify.DataAccess.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.Commons.Exceptions;
using Classify.Service.Commons.Helper.Security;
using Classify.Service.DTOs.LoginDto;
using Classify.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Classify.Service.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly IUserService userService;

    public AuthService(IConfiguration configuration, IUserService userService)
    {
        this.configuration = configuration;
        this.userService = userService;
    }



    public string GenerateToken(User user)
    { 
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Region", user.Region.ToString()),
                new Claim("School", user.School.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task<LoginResultDto> AuthenticateAsync(string email, string password)
    {
        var user = await this.userService.SelectAsync(user => user.Email == email);
        if (user == null || PasswordHasher.Verify(password, user.PasswordHash))
            throw new CustomerException(400, "Email or Password went wrong");

        return new LoginResultDto()
        {
            Token = GenerateToken(user)
    };
    }

    public ValueTask<string> GenerateToken(string email, string password)
    {
        throw new NotImplementedException();
    }
}
