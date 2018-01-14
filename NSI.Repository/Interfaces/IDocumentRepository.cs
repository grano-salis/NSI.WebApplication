using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using NSI.REST.Models;

namespace NSI.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        DocumentDetails GetDocument(int documentId);
        int SaveDocument(CreateDocumentDto document);
        PagingResultModel<DocumentDetails> GetAllDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        int Update(DocumentDto document);
        List<DocumentDetails> GetAllDocuments();
        List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id);
        List<DocumentDto> GetDocumentsByCase(int id);
        int GetNumberOfDocumentsByCase(int caseId);
        List<DocumentCategoryNamesDto> GetDocumentCategories();
    }
}
