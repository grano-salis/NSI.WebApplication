using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class CreateDocumentDto
    {
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public string DocumentTitle { get; set; }
        [DataMember]
        public string DocumentDescription { get; set; }
        [DataMember]
        public int CaseId { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public int CreatedByUserId { get; set; }
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string DocumentContent { get; set; }
    }
}
