﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IkarusEntities
{
    public partial class Email
    {
        public int EmailId { get; set; }
        public int? ContactId { get; set; }
        public string EmailAddress { get; set; }
        public Contact Contact { get; set; }
    }
}