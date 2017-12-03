using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public int CaseId { get; set; }
        public int DocumentCategoryId { get; set; }
        public int FileTypeId { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentContent { get; set; }
        public int? CreatedByUserId { get; set; }
        public string Description { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public FileType FileType { get; set; }
    }
}
