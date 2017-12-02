using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class AddressCreateModel
    {
        public int AddressId { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int AddressTypeId { get; set; }
        public int CreatedByUserId { get; set; }
    }

    public class AddressEditModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int AddressTypeId { get; set; }
    }
}
