using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class TasksCreateModel
    {
        public int TaskId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class TasksEditModel
    {
        public int TaskId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        public string Status { get; set; }
    }

    public class TasksSearchModel
    {
        public long? TaskId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? UserId { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
