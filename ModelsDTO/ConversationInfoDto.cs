﻿using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationInfoDTO
    {
        public int ConverastionId { get; set; }
        public IEnumerable<UserConversation> Owners { get; set; }
        public virtual IEnumerable<MessageDTO> Messages { get; set; }
    }
}
