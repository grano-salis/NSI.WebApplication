using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class ClientContact
    {
        public int ClientContactId { get; set; }
        public int ClientId { get; set; }
        public int Contact { get; set; }

        public Client Client { get; set; }
        public Contact ContactNavigation { get; set; }
    }
}
