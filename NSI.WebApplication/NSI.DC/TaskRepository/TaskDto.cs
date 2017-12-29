using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.TaskRepository
{
    [DataContract]
    public class TaskDto
    {
        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateModified { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
