using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{

    public class DocumentCategoryCreateModel
    {
        [Required]
        public string DocumentCategoryTitle { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

    public class DocumentCategoryEditModel
    {
        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryTitle { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

}
