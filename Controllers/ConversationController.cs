using FalcoBackEnd.Helpers;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Controllers
{
    [ApiController]
    [Authorize]
    [AuthAttribute]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService conversationService;

        public ConversationController(IConversationService conversationService)
        {
            this.conversationService = conversationService;
        }

        [HttpGet]
        public IActionResult GetConversation()
        {
            var response = conversationService.GetAllConversations();
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetConversationById(int id)
        {
            var response = conversationService.GetConversationByID(id);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddConversation([FromBody]params int[] owners)
        {
            var response = conversationService.AddConversation(owners);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult EditConversation(int id, [FromBody] params int[] owners)
        {
            var response = conversationService.EditConversation(id, owners);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteConversation(int id)
        {
            var response = conversationService.DeleteConversation(id);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }
    }
}
