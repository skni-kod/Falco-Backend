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
using Microsoft.EntityFrameworkCore;


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

        public async Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model)
        {
            logger.LogInformation("Executing Authenticate method");
            model.Password = await hashService.Encrypt(model.Password);

            var user = await falcoDbContext.Users.
                SingleOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) return null;

            var token = await NewToken(user);

            return new AuthenticateResponseDTO(user, token);
        }

        public async Task<string> NewToken(User user)
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

        public async Task<AuthenticateResponseDTO> AddUser(UserDTO userDto)
        {
            logger.LogInformation("Executing AddUser method");
            var user = await falcoDbContext.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);
            if (user != null)
            {
                return null;
            }

            UserDTO newUser = new UserDTO {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password 
            };
            newUser.Password = await hashService.Encrypt(newUser.Password);

            var result = falcoDbContext.Users.Add(mapper.Map<UserDTO, User>(newUser));
            await falcoDbContext.SaveChangesAsync();

            return await Authenticate(new AuthenticateRequestDTO { Email = user.Email, Password = user.Password });
        }
    }
}
