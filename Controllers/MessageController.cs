using AutoMapper;
using FalcoBackEnd.Helpers;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
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
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessageController(IMapper mapper, IMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] MessageParams messageParams)
        {
            var response = await _messageService.GetMessages(messageParams);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{messageID}")]
        public async Task<IActionResult> GetMessageById(int messageID)
        {
            var response = await _messageService.GetMessage(messageID);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] AddMessageDTO addMessageDTO, int conversationID)
        {
            var response = await _messageService.CreateMessage(addMessageDTO, conversationID);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("{messageID}")]
        public async Task<IActionResult> EditConversation(string messageContent, int messageID)
        {
            var response = await _messageService.UpdateMessage(messageContent, messageID);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{messageID}")]
        public async Task<IActionResult> DeleteConversation(int messageID)
        {
            var response = await _messageService.DeleteMessage(messageID);
            if (response == null)
            {
                return BadRequest(new { message = "smthwrg" });
            }
            return Ok(response);
        }
    }
}
