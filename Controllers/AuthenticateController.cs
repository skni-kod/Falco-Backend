using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalcoBackEnd.Services.Interfaces;

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
        [HttpGet]
        public IActionResult Authenticate(string user, string pwd)
        {
            if (tokenService.Authenticate(user, pwd))
            {
                return Ok(new { Token = tokenService.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "fck off");
                return Unauthorized(ModelState);
            }
        }
    }
}
