using FalcoBackEnd.Models;
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
        private readonly AppSettings appSettings;
        private readonly IHashService hashService;

        public TokenService(IOptions<AppSettings> appSettings, IHashService hashService)
        {
            this.appSettings = appSettings.Value;
            this.hashService = hashService;
        }

        private List<User> users = new List<User>
        {
            new User {Id = 0, FirstName="Horse", LastName="Boar", Username="admin", Password="password"}
        };

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var user = users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = NewToken(user);

            return new AuthenticateResponseDTO(user, token);
        }

        public string NewToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                                     new Claim(ClaimTypes.Name, user.Username.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }
    }
}
