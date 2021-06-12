using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalcoBackEnd.Services.Interfaces;
using FalcoBackEnd.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using FalcoBackEnd.Models;

namespace FalcoBackEnd.Controllers
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService tokenService;


        public AuthenticateController(IAuthService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public IActionResult Authenticate(AuthenticateRequestDTO model)
        {
            var response = tokenService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody]UserDTO user)
        {
            var response = tokenService.AddUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }
    }
}
