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
        public int DocumentId {get; set;}
    }
}
