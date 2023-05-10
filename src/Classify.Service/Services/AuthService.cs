using Classify.DataAccess.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.Exceptions;
using Classify.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Classify.Service.Services;

public class AuthService : IAuthService
{
    protected readonly IRepository<User> repository;
    protected readonly IConfiguration configuration;

    public AuthService(IRepository<User> repository, IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
    }

    public async ValueTask<string> GenerateToken(string email, string password)
    {
        var user = await repository.SelectAsync(u =>u.Email.Equals(email));
        if (user is null)
            throw new CustomerException(400, "Email or Password is incorrect");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Region", user.Address.ToString()),
                new Claim("School", user.School.ToString())
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
