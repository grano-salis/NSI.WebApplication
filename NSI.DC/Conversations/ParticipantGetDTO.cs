using IkarusEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class ParticipantGetDTO
    {
        public int ParticipantId { get; set; }
        public bool? IsSnoozed { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastSeenTime { get; set; }
        //public ConversationGetDTO Conversation { get; set; }
        public UserGetDTO User { get; set; }
        public int conversationId {get; set;}
        
    }
}
