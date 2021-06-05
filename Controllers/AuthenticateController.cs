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
        private readonly ITokenManager tokenManager;

        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }
        [HttpGet]
        public IActionResult Authenticate(string user, string pwd)
        {
            if (tokenManager.Authenticate(user, pwd))
            {
                return Ok(new { Token = tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "fck off");
                return Unauthorized(ModelState);
            }
        }
    }
}
