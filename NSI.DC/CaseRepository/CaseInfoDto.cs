using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.CaseRepository
{

    public class CaseInfoDto
    {
		public int CaseId { get; set; }
		public string CaseNumber { get; set; }
		public string CourtNumber { get; set; }
		public decimal? Value { get; set; }
        public char? Judge { get; set; }
        public string Court { get; set; }
        public string CounterParty { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int CaseCategory { get; set; }
        public int CustomerId { get; set; }
        public int ClientId { get; set; }
        public int CreatedByUserId { get; set; }
	}
}
