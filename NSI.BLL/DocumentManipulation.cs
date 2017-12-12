using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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


        public List<DocumentDto> GetAllDocuments()
        {
            return _documentRepository.GetAllDocuments();
        }

        public void SaveDocument(List<IFormFile> files, string filePath)
        {
            var document = new DocumentDto()
            {
                DocumentPath = filePath
            };
            _documentRepository.SaveDocument(document);
        }

        public PagingResultModel<DocumentDto> GetDocumentsByPage(DocumentsPagingQueryModel query)
        {
            return _documentRepository.GetAllDocumentsByPage(query);
        }

        public DocumentDto GetDocumentById(int documentId)
        {
            return _documentRepository.GetDocument(documentId);
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
