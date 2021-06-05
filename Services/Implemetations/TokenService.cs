using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Implemetations
{
    public class TokenService : ITokenService
    {
        private readonly AppSettingsDTO appSettings;
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;

        public TokenService(IOptions<AppSettingsDTO> appSettings)
        {
            this.appSettings = appSettings.Value;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool Authenticate(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) &&
                !string.IsNullOrWhiteSpace(password) &&
                username.ToLower() == "admin" &&
                password == "password")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string NewToken()
        {
            secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "KonDzik") }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var claims = tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
               }, out SecurityToken validatedToken);
            return claims;
        }
    }
}
