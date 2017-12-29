using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using IkarusEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSI.REST.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDetails GetDocument(int documentId);
        IEnumerable<DocumentDto> SearchDocuments(DocumentSearchCriteriaDto searchCriteria);
        int SaveDocument(DocumentDto document);
        PagingResultModel<DocumentDetails> GetAllDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        int Update(DocumentDto document);
        List<DocumentDetails> GetAllDocuments();
        List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id);
        List<DocumentDto> GetDocumentsByCase(int id);
        int GetNumberOfDocumentsByCase(int caseId);
    }
}
