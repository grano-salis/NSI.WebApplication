using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.AdminRepository
{
    [DataContract]
    public class CaseCategoryDto
    {
        [DataMember]
        public int CaseCategoryId { get; set; }

        [DataMember]
        public string CaseCategoryName { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }

        [DataMember]
        public int? CustomerId { get; set; }
    }
}
