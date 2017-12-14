using System;
using NSI.BLL.Interfaces;
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

        public PagingResultModel<DocumentDto> GetDocumentsByPage(DocumentsPagingQueryModel query)
        {
            return _documentRepository.GetAllDocuments(query);
        }

        public DocumentDto GetDocumentById(int documentId)
        {
            return _documentRepository.GetDocument(documentId);
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
