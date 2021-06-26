using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationDTO
    {
        public int Converastion_id { get; set; }
        public string Owners { get; set; }
        public int[] OwnersList { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public ConversationDTO(string OwnersString)
        {
            this.Owners = OwnersString;
            this.OwnersList = OwnersString.Split(' ').Select(int.Parse).ToArray();
        }
        public ConversationDTO(int[] OwnersList)
        {
            this.OwnersList = OwnersList;
            string ownersString = "";
            foreach (int owner in OwnersList)
            {
                ownersString += owner.ToString() + " ";
            }
            ownersString = ownersString.TrimEnd();
            this.Owners = ownersString;
        }
    }
}
