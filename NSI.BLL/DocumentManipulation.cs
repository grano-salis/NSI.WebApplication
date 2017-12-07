using System;
using System.Collections;
using IkarusEntities;
using NSI.BLL.Interfaces;
using NSI.DC.Common;
using NSI.DC.DocumentRepository;
using NSI.Repository.Interfaces;
using NSI.REST.Models;

namespace NSI.BLL
{
    public class DocumentManipulation : IDocumentManipulation
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentManipulation(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public PagingResultModel<DocumentDto> GetDocumentsByPage(REST.Models.DocumentsPagingQueryModel query)
        {
            return _documentRepository.GetAllDocuments(query);
        }

        public ICollection GetCaseDocuments(int caseId)
        {
            throw new NotImplementedException();
        }

        public ICollection GetCustomerDocuments(int customerId, Paging paging)
        {
            throw new NotImplementedException();
        }

        public DocumentDto GetDocumentById(int documentId)
        {
            throw new NotImplementedException();
        }

        public DocumentDto SaveDocument()
        {
            throw new NotImplementedException();
        }

        public bool DeleteDocument(int id)
        {
            return _documentRepository.DeleteDocument(id);
        }

        public bool EditDocument(int id, DocumentDto documentDto)
        {
            var document = _documentRepository.GetDocument(id);
            if (document == null) return false;
            document.LastModified = DateTime.UtcNow;
            _documentRepository.Update(document);
            return true;
        }
    }
}
