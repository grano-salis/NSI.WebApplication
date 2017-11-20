using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Participant
    {
        public int ParticipantId { get; set; }
        public bool? IsSnoozed { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime LastSeenTime { get; set; }
        public int? UserId { get; set; }
        public int? ConversationId { get; set; }

        public Conversation Conversation { get; set; }
        public UserInfo User { get; set; }
    }
}
