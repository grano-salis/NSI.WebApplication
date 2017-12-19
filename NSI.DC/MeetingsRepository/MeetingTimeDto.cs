using NSI.DC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
namespace NSI.DC.MeetingsRepository
{
    [DataContract]
    public class MeetingTimeDto
    {
        [DataMember]
        [Required]
        [BeforeEndDate(EndDatePropertyName = "To")]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime? To { get; set; }
    }
}
