using NSI.DC.DocumentRepository;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDto GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        long SaveDocument(DocumentDto document);
        REST.Models.DocumentsPagingResultModel GetAllDocuments(REST.Models.DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        void Update(DocumentDto document);
    }
}
