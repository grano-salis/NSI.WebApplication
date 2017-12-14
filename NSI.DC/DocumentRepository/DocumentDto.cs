 using System;
 using System.Runtime.Serialization;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentDto
    {
        [DataMember]
        public int DocumentId {get; set;}
        [DataMember]
        public int FileTypeExtension { get; set; }
        [DataMember]
        public string DocumentTitle { get; set; }
        [DataMember]
        public int CaseId { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public string DocumentDescription { get; set; }
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string DocumentContent { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime LastModified { get; set; }
    }
}
