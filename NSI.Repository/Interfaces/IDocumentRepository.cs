using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using System.Linq;
using NSI.REST.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDto GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        long SaveDocument(DocumentDto document);
        PagingResultModel<DocumentDto> GetAllDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        void Update(DocumentDto document);
        List<DocumentDto> GetAllDocuments();
    }
}
