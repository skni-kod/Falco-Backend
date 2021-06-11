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
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var response = userService.GetAll();
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            var response = userService.GetById(id);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrng" });
            }
            return Ok(response);
        }

        [HttpPut]
        public IActionResult EditUser([FromBody]UserDTO user)
        {
            var response = userService.EditUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] UserDTO user)
        {
            var response = userService.DeleteUser(user);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

    }
}
