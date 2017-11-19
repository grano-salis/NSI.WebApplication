using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.DocumentRepository
{
    [DataContract]
    public class DocumentSearchCriteriaDto
    {
        [DataMember]
        int? DocumentId { get; set; }

        [DataMember]
        int? CaseId { get; set; }

        [DataMember]
        int? CustomerId { get; set; }

        [DataMember]
        Tuple<string, bool> DocumentNameCriteria { get; set; }

        [DataMember]
        bool IsExactMatchDocumentName { get; set; }
    }
}
