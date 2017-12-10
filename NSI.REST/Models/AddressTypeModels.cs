using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class AddressTypeCreateModel
    {
        public int AddressTypeId { get; set; }
        [Required]
        public string AddressTypeName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class AddressTypeEditModel
    {
        public int AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
