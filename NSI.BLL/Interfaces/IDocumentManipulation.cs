using NSI.DC.DocumentRepository;
using NSI.REST.Models;

namespace NSI.BLL.Interfaces
{
    public interface IDocumentManipulation
    {
        DocumentDto GetDocumentById(int documentId);
        PagingResultModel<DocumentDto> GetDocumentsByPage(DocumentsPagingQueryModel query);
        DocumentDto SaveDocument();
        bool DeleteDocument(int id);
        bool EditDocument(int id, DocumentDto documentDto);
    }
}
