using AutoMapper;
using AutoMapper.QueryableExtensions;
using FalcoBackEnd.Helpers;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Implemetations
{
    public class MessageService : IMessageService
    {
        private readonly FalcoDbContext _falcoDbContext;
        private readonly IMapper _mapper;

        public MessageService(FalcoDbContext falcoDbContext, IMapper mapper)
        {
            _falcoDbContext = falcoDbContext;
            _mapper = mapper;
        }

        public async Task<MessageDTO> CreateMessage(AddMessageDTO addMessageDTO, int conversationID)
        {
            var message = _mapper.Map<Message>(addMessageDTO);
            message.Conversation_id = conversationID;
            message.CreateDate = DateTime.UtcNow;
            message.IsDeleted = false;
            message.IsEdited = false;


            await _falcoDbContext.AddAsync(message);
            await _falcoDbContext.SaveChangesAsync();

            var messageDTO = _mapper.Map<MessageDTO>(message);
            return messageDTO;
        }

        public async Task<MessageDTO> DeleteMessage(int messageID)
        {
            Message message = await _falcoDbContext.Messages.SingleOrDefaultAsync(m => m.Message_id == messageID);

            var messageDTO = _mapper.Map<MessageDTO>(message);

            message.IsDeleted = true;
            await _falcoDbContext.SaveChangesAsync();

            return messageDTO;
        }

        public async Task<MessageDTO> GetMessage(int messageID)
        {
            var message = await _falcoDbContext.Messages
                .SingleOrDefaultAsync(m => m.Message_id == messageID);

            if (message == null) return null;
            var messageDTO = _mapper.Map<MessageDTO>(message);

            return messageDTO;
        }

        public async Task<PagedList<MessageDTO>> GetMessages(MessageParams messageParams)
        {
            var messages = _falcoDbContext.Messages.Where(m => m.Conversation_id == messageParams.conversationID).AsNoTracking();

            return await PagedList<MessageDTO>.ToPagedListAsync(messages.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
            , messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<MessageDTO> UpdateMessage(string messageContent, int messageID)
        {
            var message = await _falcoDbContext.Messages.SingleOrDefaultAsync(m => m.Message_id == messageID);

            message.Content = messageContent;
            message.IsEdited = true;

            await _falcoDbContext.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(message);
        }
    }
}
