using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Xml.Linq;
using TechChallengeBlogWebApi.Factory;

namespace TechChallengeBlogWebApi.Util
{
    public static class JwtBearerExtensions
    {
        public static void ConfigureRsaSecurityKeyFactory(this JwtBearerOptions options, IConfiguration configuration)
        {
            RsaSecurityKeyFactory rsaSecurityKeyFactory = new(configuration);

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidAudience = configuration["Jwt:ValidAudience"],
                IssuerSigningKey = rsaSecurityKeyFactory.Create()
            };
            options.EventsType = typeof(JwtSecurityExtensionEvents);
        }
    }
}