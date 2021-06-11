using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Implemetations
{
    public class HashService : IHashService
    {
        private readonly AppSettings appSettings;
        private readonly ILogger logger;
        public HashService(IOptions<AppSettings> appSettings,
                            ILogger<HashService> logger)
        {
            this.appSettings = appSettings.Value;
            this.logger = logger;
        }
        public string Encrypt(string password)
        {
            logger.LogInformation("Executing Encrypt method");

            if (string.IsNullOrEmpty(password)) return "";
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(appSettings.Secret),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 256 / 8
                ));
        }
    }
}
