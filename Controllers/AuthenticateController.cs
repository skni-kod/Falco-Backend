using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalcoBackEnd.Services.Interfaces;
using FalcoBackEnd.ModelsDTO;

namespace FalcoBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public AuthenticateController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequestDTO model)
        {
            var response = tokenService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
