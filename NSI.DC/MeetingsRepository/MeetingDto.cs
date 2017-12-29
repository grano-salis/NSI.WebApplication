using NSI.DC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.MeetingsRepository
{
    [DataContract]
    public class MeetingDto
    {
        [DataMember]
        public int MeetingId { get; set; }

        [DataMember]
        [Required]
        [StringLength(120, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9 ]*$")]
        public string Title { get; set; }

        [DataMember]
        [Required]
        [StringLength(120, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9 ]*$")]
        public string MeetingPlace { get; set; }

        [DataMember]
        [Required]
        [BeforeEndDate(EndDatePropertyName="To")]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime? To { get; set; }

        [DataMember]
        [EnsureOneElement(ErrorMessage = "At least one user is required")]
        public IEnumerable<UserMeetingDto> UserMeeting { get; set; }
    }
}
