using System;
using System.Collections.Generic;
using System.Text;

namespace IkarusEntities
{
    public partial class Phone
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public int? ContactId { get; set; }
        public bool? IsDeleted { get; set; }
        public Contact Contact { get; set; }
    }
}
