using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.CaseRepository
{
	[DataContract]
    public class CaseInfoDto
    {
		[DataMember]
		public int CaseId { get; set; }

		[DataMember]
		public string CaseNumber { get; set; }

		[DataMember]
		public string CourtNumber { get; set; }

		[DataMember]
		public decimal? Value { get; set; }

        [DataMember]
        public char? Judge { get; set; }

        [DataMember]
        public string Court { get; set; }

        [DataMember]
        public string CounterParty { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateModified { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public int CaseCategory { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int CreatedByUserId { get; set; }
	}
}
