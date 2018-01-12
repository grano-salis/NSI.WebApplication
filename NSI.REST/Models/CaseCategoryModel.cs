using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{

    public class CaseCategoryCreateModel
    {
        [Required]
        public string CaseCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

    public class CaseCategoryEditModel
    {
        public int CaseCategoryId { get; set; }
        public string CaseCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

}
