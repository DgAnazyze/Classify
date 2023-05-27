using Classify.DataAccess.Interfaces;
using Classify.DataAccess.Repositories;
using Classify.Service.Interfaces;
using Classify.Domain.Entities;
using Classify.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Classify.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddService(this IServiceCollection services)
        { 
            services.AddScoped<IExcelReaderService, ExcelReaderService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();   
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
        #region JWT
        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }
        #endregion

        #region Setup Swagger
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ahsan.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
        }
        #endregion
    }
}

