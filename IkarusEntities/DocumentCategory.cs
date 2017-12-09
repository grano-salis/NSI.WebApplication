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

        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryTitle { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Document> Document { get; set; }
    }
}
