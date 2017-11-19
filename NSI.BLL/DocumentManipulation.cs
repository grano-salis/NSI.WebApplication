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
    }
}
