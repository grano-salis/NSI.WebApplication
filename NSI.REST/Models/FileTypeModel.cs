using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{

    public class FileTypeCreateModel
    {
        [Required]
        public string Extension { get; set; }
        [Required]
        public string IconPath { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
    }

    public class FileTypeEditModel
    {
        public int FileTypeId { get; set; }
        public string Extension { get; set; }
        public string IconPath { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

}
