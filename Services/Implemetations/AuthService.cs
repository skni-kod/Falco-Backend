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
    public class AuthService : IAuthService
    {
        private readonly AppSettings appSettings;
        private readonly IHashService hashService;
        private readonly FalcoDbContext falcoDbContext;

        public AuthService(IOptions<AppSettings> appSettings,
                            IHashService hashService,
                            FalcoDbContext falcoDbContext)
        {
            this.appSettings = appSettings.Value;
            this.hashService = hashService;
            this.falcoDbContext = falcoDbContext;
        }

        private List<User> users = new List<User>
        {
            new User {Id = 0, FirstName="Horse", LastName="Boar", Email="admin@gmail.com", Password="password"}
        };

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var user = users.SingleOrDefault(x => x.Email == model.Username && x.Password == model.Password);

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
                                                     new Claim(ClaimTypes.Email, user.Email.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }

        public ResponseDTO AddUser(UserDTO userDTO)
        {
            var newUser = new User { Email = userDTO.Email, FirstName = userDTO.FirstName, LastName = userDTO.LastName, Password = userDTO.Password, Id = userDTO.Id };
            try
            {
                falcoDbContext.Users.Add(newUser);
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Failed" };
            }
            return new ResponseDTO() { Code = 200, Message = "Added user to DB", Status = "Success" };
        }
    }
}
