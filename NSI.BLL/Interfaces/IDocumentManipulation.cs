using System.Collections.Generic;
using IkarusEntities;
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
        bool EditDocument(int id, DocumentDto documentDto);
        List<DocumentDetails> GetAllDocuments();
        void UploadFile(List<IFormFile> files, string filePath);
        bool SaveDocument(DocumentDto document);
        List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id);
    }
}
