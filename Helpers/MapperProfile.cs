using AutoMapper;
using FalcoBackEnd.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Models.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<ConversationDTO, Conversation>();
            CreateMap<Conversation, ConversationInfoDTO>()
                .ForMember(dto => dto.Owners, opt => opt.MapFrom(x => x.Owners.Select(y => y.User).ToList()));

            CreateMap<ConversationInfoDTO, Conversation>();
            CreateMap<UserConversation, UserInfoDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserInfoDTO>(); 
            CreateMap<AddUserDTO, User>();
            CreateMap<Message, MessageDTO>();
        }
    }
}
