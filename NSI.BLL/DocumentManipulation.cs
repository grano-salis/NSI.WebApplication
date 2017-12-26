using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.Common;
using NSI.DC.DocumentRepository;
using System.Collections;
using NSI.Repository;
using NSI.Repository.Interfaces;

namespace NSI.BLL.DocumentRepository
{
    public class DocumentManipulation : IDocumentManipulation
    {
        IDocumentRepository _documentRepository;
        public DocumentManipulation(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public REST.Models.DocumentsPagingResultModel GetDocumentsByPage(REST.Models.DocumentsPagingQueryModel query)
        {
            return _documentRepository.GetAllDocuments(query);
            throw new NotImplementedException();
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
        public int GetDocumentsByCase(int caseId)
        {
           
            return _documentRepository.GetDocumentsByCase(caseId);
        }
    }
}
