using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class ConversationPostDTO
    {
        public int loggedUserId { get; set; }
        public List<int> usersToParticipants { get; set; }
        public string conversationName { get; set; }

    }
}
