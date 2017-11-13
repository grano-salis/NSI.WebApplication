using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            Document = new HashSet<Document>();
        }

        public long DocumentCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Document> Document { get; set; }
    }
}
