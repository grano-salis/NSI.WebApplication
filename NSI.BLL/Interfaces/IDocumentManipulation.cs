using System.Collections;
using NSI.DC.Common;
using NSI.DC.DocumentRepository;

namespace NSI.BLL.Interfaces
{
    public interface IDocumentManipulation
    {
        DocumentDto GetDocumentById(int documentId);
        ICollection GetCaseDocuments(int caseId);
        ICollection GetCustomerDocuments(int customerId, Paging paging);
        REST.Models.DocumentsPagingResultModel GetDocumentsByPage(REST.Models.DocumentsPagingQueryModel query);
        DocumentDto SaveDocument();
        bool DeleteDocument(int id);
        bool EditDocument(int id, DocumentDto documentDto);
    }
}
