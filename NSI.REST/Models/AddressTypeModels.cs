using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class AddressTypeCreateModel
    {
        [Required]
        public string AddressTypeName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CustomerId { get; set; }
    }

    public class AddressTypeEditModel
    {
        public string AddressTypeName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CustomerId { get; set; }
    }
}
