using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using IkarusEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSI.REST.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDto GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        int SaveDocument(DocumentDto document);
        PagingResultModel<DocumentDto> GetAllDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        void Update(DocumentDto document);
        List<DocumentDto> GetAllDocuments();
    }
}
