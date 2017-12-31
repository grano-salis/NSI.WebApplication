using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.Conversations
{
    public class ParticipantPostDTO
    {
        public int conversationId { get; set; }
        public List<int> usersToParticipant { get; set; }
    }
}
