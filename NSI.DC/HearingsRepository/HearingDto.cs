using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.HearingsRepository
{
    [DataContract]
    public class HearingDto
    {
        [DataMember]
        public int HearingId { get; set; }

        [DataMember]
        public DateTime HearingDate { get; set; }

        [DataMember]
        public int CreatedByUserId { get; set; }

        [DataMember]
        public int CaseId { get; set; }
        
        [DataMember]
        public IEnumerable<UserHearingDto> UserHearing { get; set; }

        [DataMember]
        public IEnumerable<NoteDto> Note { get; set; }
    }
}
