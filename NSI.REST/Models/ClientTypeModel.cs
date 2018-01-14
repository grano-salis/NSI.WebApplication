using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{

    public class ClientTypeCreateModel
    {
        [Required]
        public string ClientTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

    public class ClientTypeEditModel
    {
        public int ClientTypeId { get; set; }
        public string ClientTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int? CustomerId { get; set; }
    }

}
