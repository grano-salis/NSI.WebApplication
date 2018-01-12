using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.AdminRepository
{
    [DataContract]
    public class DocumentCategoryDto
    {
        [DataMember]
        public int DocumentCategoryId { get; set; }

        [DataMember]
        public string DocumentCategoryTitle { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public int? CustomerId { get; set; }

    }
}
