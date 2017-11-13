using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class ClientContact
    {
        public long ClientContactId { get; set; }
        public long ClientId { get; set; }
        public long Contact { get; set; }

        public Client Client { get; set; }
        public Contact ContactNavigation { get; set; }
    }
}
