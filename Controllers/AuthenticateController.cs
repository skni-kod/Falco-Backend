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
        public async Task<IActionResult> Authenticate(AuthenticateRequestDTO model)
        {
            var response = await tokenService.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] AddUserDTO AddUserDTO)
        {
            var response = await tokenService.AddUser(AddUserDTO);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }
    }
}
