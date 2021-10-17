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
            CreateMap<Conversation, ConversationInfoDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserInfoDTO>();
            CreateMap<AddUserDTO, User>();
        }
    }
}
