using IkarusEntities;
using NSI.DC.DocumentRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository
{
    public partial class DocumentRepository
    {
        public static Document MapToDbEntity(DocumentDto document)
        {
            return new Document()
            {
                CaseId = document.DocumentId,
                DocumentId = document.DocumentId
            };
        }

        public static DocumentDto MapToDto(Document document)
        {
            return new DocumentDto()
            {
                DocumentId = document.DocumentId
            };
        }

        
    }
}
