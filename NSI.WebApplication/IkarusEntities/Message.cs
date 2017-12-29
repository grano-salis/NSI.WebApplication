using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string Message1 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ConversationId { get; set; }
        public int? CreatedByUserId { get; set; }

        public Conversation Conversation { get; set; }
        public UserInfo CreatedByUser { get; set; }
    }
}
