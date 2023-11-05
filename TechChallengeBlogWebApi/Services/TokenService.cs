using App.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TechChallengeBlogWebApi.Interfaces;

#nullable disable

namespace TechChallengeBlogWebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ISistemaService _sistemaService;
        private readonly IRsaSecurityKeyFactory _rsaSecurityKeyFactory;

        public TokenService(IConfiguration configuration, ISistemaService sistemaService, IRsaSecurityKeyFactory rsaSecurityKeyFactory)
        {
            _configuration = configuration;
            _sistemaService = sistemaService;
            _rsaSecurityKeyFactory = rsaSecurityKeyFactory;
        }

        public async Task<string> GerarTokenAsync(string chave)
        {
            try
            {
                bool existe = _sistemaService.VerificarChaveExiste(chave);

                if (!existe)
                {
                    return await Task.Run(() => string.Empty);
                }

                JwtSecurityTokenHandler tokenHandler = new();

                SigningCredentials signingCredentials = new(key: _rsaSecurityKeyFactory.Create(), algorithm: SecurityAlgorithms.RsaSha256);

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Audience = _configuration["Jwt:ValidAudience"],
                    Issuer = _configuration["Jwt:ValidIssuer"],
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = signingCredentials,
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "System.Fiap.Api") })
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string jwtToken = tokenHandler.WriteToken(token);

                return await Task.Run(() => jwtToken);
            }
            catch
            {
                throw;
            }
        }
    }
}