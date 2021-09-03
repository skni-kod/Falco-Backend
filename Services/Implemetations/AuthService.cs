using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public AuthService(IOptions<AppSettings> appSettings,
                            IHashService hashService,
                            FalcoDbContext falcoDbContext,
                            ILogger<AuthService> logger,
                            IMapper mapper)
        {
            this.appSettings = appSettings.Value;
            this.hashService = hashService;
            this.falcoDbContext = falcoDbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            logger.LogInformation("Executing Authenticate method");
            model.Password = hashService.Encrypt(model.Password);

            var user = falcoDbContext.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) return null;

            var token = NewToken(user);

            return new AuthenticateResponseDTO(user, token);
        }

        public string NewToken(User user)
        {
            logger.LogInformation("Executing NewToken method");

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                                     new Claim(ClaimTypes.Email, user.Email.ToString())}),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }

        public AuthenticateResponseDTO AddUser(UserDTO user)
        {
            logger.LogInformation("Executing AddUser method");

            if (falcoDbContext.Users.Where(u => u.Email == user.Email).Any())
            {
                throw new InvalidOperationException($"User with email {user.Email} already exist in db");
            }

            UserDTO newUser = new UserDTO { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password };
            newUser.Password = hashService.Encrypt(newUser.Password);

            try
            {
                var result = falcoDbContext.Users.Add(mapper.Map<UserDTO, User>(newUser));
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
            return Authenticate(new AuthenticateRequestDTO { Email = user.Email, Password = user.Password });
        }
    }
}
