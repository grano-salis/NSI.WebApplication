 using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentDto
    {
        [DataMember]
        public long DocumentId {get; set;}
    }
}
