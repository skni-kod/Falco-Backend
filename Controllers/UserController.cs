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
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var response = await userService.AddUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await userService.GetAllUsers();
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await userService.GetUserById(id);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody]UserDTO user)
        {
            var response = await userService.EditUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserDTO user)
        {
            var response = await userService.DeleteUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

    }
}
