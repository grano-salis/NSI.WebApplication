using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.AdminRepository
{
    [DataContract]
    public class FileTypeDto
    {
        [DataMember]
        public int FileTypeId { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public string IconPath { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }
    }
}
