using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.HearingsRepository
{
    public class UserHearingDto
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
