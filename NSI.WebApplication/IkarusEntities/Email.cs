using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Email
    {
        public int EmailId { get; set; }
        public int ContactId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }

        public Contact Contact { get; set; }
    }
}
