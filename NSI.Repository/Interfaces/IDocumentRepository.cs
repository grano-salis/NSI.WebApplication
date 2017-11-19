using NSI.DC.DocumentRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDto GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        long SaveDocument(DocumentDto document);
        
    }
}
