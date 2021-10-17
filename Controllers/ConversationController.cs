using AutoMapper;
using FalcoBackEnd.Helpers;
using FalcoBackEnd.Models;
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
    //[Authorize]
    //[AuthAttribute]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService conversationService;
        private readonly IMapper mapper;

        public ConversationController(IConversationService conversationService, IMapper mapper)
        {
            this.conversationService = conversationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetConversation()
        {
            var response = await conversationService.GetAllConversations();
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetConversationById(int id)
        {
            var response = await conversationService.GetConversationByID(id);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddConversation([FromBody] ICollection<AddConversationDTO> users)
        {
            var response = await conversationService.AddConversation(users);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditConversation(int id, [FromBody] ICollection<int> users)
        {
            var response = await conversationService.EditConversation(id, users);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{conversationID}")]
        public async Task<IActionResult> DeleteConversation(int conversationID)
        {
            var response = await conversationService.DeleteConversation(conversationID);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }
    }
}
