using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSI.DC.DocumentRepository;
using NSI.REST.Models;

namespace NSI.BLL.Interfaces
{
    public interface IDocumentManipulation
    {
        DocumentDetails GetDocumentById(int documentId);
        PagingResultModel<DocumentDetails> GetDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        DocumentDetails EditDocument(int id, DocumentDto documentDto);
        List<DocumentDetails> GetAllDocuments();
        Task<string> UploadFileAsync(IFormFile file);
        bool SaveDocument(CreateDocumentDto document);
        List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id);
        List<DocumentDto> GetDocumentsByCase(int id);
        int GetNumberOfDocumentsByCase(int caseId);
        List<DocumentCategoryNamesDto> GetDocumentCategories();
    }
}
