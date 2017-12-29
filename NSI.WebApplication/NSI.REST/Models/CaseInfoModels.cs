using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class CaseInfoCreateModel
    {
        public int CaseId { get; set; }

        [Required]
        public string CaseNumber { get; set; }

        [Required]
        public string CourtNumber { get; set; }

        public decimal? Value { get; set; }

        public char? Judge { get; set; }

        public string Court { get; set; }

        public string CounterParty { get; set; }

        public string Note { get; set; }

        [Required]
        public int CaseCategory { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }



    }

    public class CaseInfoEditModel
    {
        public int CaseId { get; set; }

        [Required]
        public string CaseNumber { get; set; }

        [Required]
        public string CourtNumber { get; set; }

        [Required]
        public decimal? Value { get; set; }

        [Required]
        public char? Judge { get; set; }

        [Required]
        public string Court { get; set; }

        [Required]
        public string CounterParty { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public int CaseCategory { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

    }

}
