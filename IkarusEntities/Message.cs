using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Message
    {
        public long MessageId { get; set; }
        public string Message1 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ConversationId { get; set; }
        public long? CreatedByUserId { get; set; }

        public Conversation Conversation { get; set; }
        public UserInfo CreatedByUser { get; set; }
    }
}
