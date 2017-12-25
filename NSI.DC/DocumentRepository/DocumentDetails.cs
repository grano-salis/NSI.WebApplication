 using System;
 using System.Runtime.Serialization;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentDetails
    {
        [DataMember]
        public int DocumentId {get; set;}
        [DataMember]
        public string DocumentTitle { get; set; }
        [DataMember]
        public string DocumentDescription { get; set; }

        [DataMember]
        public int CaseId { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public int FileTypeId { get; set; }

        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string DocumentContent { get; set; }

        [DataMember]
        public int CreatedByUserId { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string DocumentCategoryName { get; set; }
        [DataMember]
        public string FileIconPath { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
    }
}
