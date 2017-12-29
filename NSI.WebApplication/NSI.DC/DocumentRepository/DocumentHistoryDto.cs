 using System;
 using System.Runtime.Serialization;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentHistoryDto
    {
        [DataMember]
        public int DocumentHistoryId { get; set; }
        [DataMember]
        public int ModifiedByUserId { get; set; }
        [DataMember]
        public DateTime ModifiedAt { get; set; }
        [DataMember]
        public int DocumentId { get; set; }
        [DataMember]
        public string ModifiedByUser { get; set; }
    }
}
