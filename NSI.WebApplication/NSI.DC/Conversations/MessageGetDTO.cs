using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class MessageGetDTO
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        // public ConversationGetDTO Conversation { get; set; }  
        public int conversationId { get; set; }
    }
}
