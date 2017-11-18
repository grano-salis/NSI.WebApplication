using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentCategoryDto
    {
        [DataMember]
        long DocumentCategoryId { get; set; }

        [DataMember]
        string DocumentCategoryName { get; set; }
    }
}
