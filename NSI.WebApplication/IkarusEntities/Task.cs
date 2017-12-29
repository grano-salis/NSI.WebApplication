using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }

        public UserInfo User { get; set; }
    }
}
