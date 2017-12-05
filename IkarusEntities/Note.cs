using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Note
    {
        public int NoteId { get; set; }
        public string Text { get; set; }
        public int HearingId { get; set; }
        public int CreatedByUserId { get; set; }

        public UserInfo CreatedByUser { get; set; }
        public Hearing Hearing { get; set; }
    }
}
