 using System;
 using System.Runtime.Serialization;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentHistoryDto
    {
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string DocumentTitle { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string DocumentCategoryName { get; set; }
        [DataMember]
        public string DocumentDescription { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public string IconPath { get; set; }
    }
}
