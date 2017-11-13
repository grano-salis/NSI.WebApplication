using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class CaseInfo
    {
        public CaseInfo()
        {
            CaseContact = new HashSet<CaseContact>();
            Document = new HashSet<Document>();
            Hearing = new HashSet<Hearing>();
            UserCase = new HashSet<UserCase>();
        }

        public long CaseId { get; set; }
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
        public long CaseCategory { get; set; }
        public long CustomerId { get; set; }
        public long ClientId { get; set; }
        public long CreatedByUserId { get; set; }

        public CaseCategory CaseCategoryNavigation { get; set; }
        public Client Client { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CaseContact> CaseContact { get; set; }
        public ICollection<Document> Document { get; set; }
        public ICollection<Hearing> Hearing { get; set; }
        public ICollection<UserCase> UserCase { get; set; }
    }
}
