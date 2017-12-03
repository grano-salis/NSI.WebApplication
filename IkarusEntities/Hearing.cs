using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Hearing
    {
        public Hearing()
        {
            Note = new HashSet<Note>();
            UserHearing = new HashSet<UserHearing>();
        }

        public int HearingId { get; set; }
        public DateTime HearingDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int CaseId { get; set; }
        public int CreatedByUserId { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public ICollection<Note> Note { get; set; }
        public ICollection<UserHearing> UserHearing { get; set; }
    }
}
