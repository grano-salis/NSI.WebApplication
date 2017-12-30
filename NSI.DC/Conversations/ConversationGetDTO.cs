using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class ConversationGetDTO
    {
        public int ConversationId { get; set; }
        public string ConversationName { get; set; }
        public ICollection<MessageGetDTO> Message { get; set; }
        public ICollection<ParticipantGetDTO> Participant { get; set; }
        public int UserId { get; set; }
    }
}
