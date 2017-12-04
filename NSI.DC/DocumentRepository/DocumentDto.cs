 using System;
 using System.Runtime.Serialization;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentDto
    {
        [DataMember]
        public int DocumentId {get; set;}
        public int FileTypeExtension { get; set; }
        public string DocumentTitle { get; set; }
        public int CaseId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        public string DocumentDescription { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentContent { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
