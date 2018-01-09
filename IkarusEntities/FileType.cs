using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class FileType
    {
        public FileType()
        {
            Document = new HashSet<Document>();
        }

        public int FileTypeId { get; set; }
        public string Extension { get; set; }
        public string IconPath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<Document> Document { get; set; }
    }
}
