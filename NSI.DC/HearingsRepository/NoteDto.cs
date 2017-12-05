using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.DC.HearingsRepository
{
    public class NoteDto
    {
        public string Text { get; set; }
        public int CreatedByUserId { get; set; }
        public int HearingId { get; set; }
    }
}
