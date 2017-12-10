using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using IkarusEntities;
using NSI.REST.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDto GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        long SaveDocument(DocumentDto document);
        PagingResultModel<DocumentDto> GetAllDocuments(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        void Update(DocumentDto document);
    }
}
