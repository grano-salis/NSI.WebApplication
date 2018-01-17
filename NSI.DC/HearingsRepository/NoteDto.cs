using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.HearingsRepository
{
    public class NoteDto
    {
        [DataMember]
        [Required]
        public string Text { get; set; }

        [DataMember]
        public int CreatedByUserId { get; set; }

        [DataMember]
        public int HearingId { get; set; }
    }
}
