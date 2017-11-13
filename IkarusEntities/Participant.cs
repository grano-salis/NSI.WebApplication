using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Participant
    {
        public long ParticipantId { get; set; }
        public bool? IsSnoozed { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime LastSeenTime { get; set; }
        public long? UserId { get; set; }
        public long? ConversationId { get; set; }

        public Conversation Conversation { get; set; }
        public UserInfo User { get; set; }
    }
}
