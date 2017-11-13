using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Document
    {
        public long DocumentId { get; set; }
        public long CaseId { get; set; }
        public long DocumentCategoryId { get; set; }
        public long FileTypeId { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentContent { get; set; }
        public long? CreatedByUserId { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public FileType FileType { get; set; }
    }
}
