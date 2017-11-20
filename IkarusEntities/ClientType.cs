using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class ClientType
    {
        public ClientType()
        {
            Client = new HashSet<Client>();
        }

        public int ClientTypeId { get; set; }
        public string ClientTypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Client> Client { get; set; }
    }
}
