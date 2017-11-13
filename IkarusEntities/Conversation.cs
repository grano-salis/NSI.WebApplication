using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Conversation
    {
        public Conversation()
        {
            Message = new HashSet<Message>();
            Participant = new HashSet<Participant>();
        }

        public long ConversationId { get; set; }
        public string ConversationName { get; set; }
        public long? UserId { get; set; }

        public UserInfo User { get; set; }
        public ICollection<Message> Message { get; set; }
        public ICollection<Participant> Participant { get; set; }
    }
}
