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


        public List<DocumentDetails> GetAllDocuments()
        {
            return _documentRepository.GetAllDocuments();
        }

        public void UploadFile(List<IFormFile> files, string filePath)
        {
            var document = new DocumentDto()
            {
                DocumentPath = filePath
            };
            //TODO
        }

        public bool SaveDocument(DocumentDto document)
        {
            if (document == null) throw new Exception("Document is null");

            var result = _documentRepository.SaveDocument(document);
            return result != null;
        }

        public List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id)
        {
            if (id == 0) throw new Exception("Id is not valid");
            return _documentRepository.GetDocumentHistoryByDocumentId(id);
        }

        public List<DocumentDto> GetDocumentsByCase(int id)
        {
            return _documentRepository.GetDocumentsByCase(id);
        }

        public PagingResultModel<DocumentDetails> GetDocumentsByPage(DocumentsPagingQueryModel query)
        {
            if(query.PageNumber < 0) throw new Exception("Page number is not valid");
            return _documentRepository.GetAllDocumentsByPage(query);
        }

        public DocumentDetails GetDocumentById(int documentId)
        {
            if (documentId == 0) throw new Exception("Id is not valid.");
            return _documentRepository.GetDocument(documentId);
        }

        public bool DeleteDocument(int id)
        {
            if (id == 0) throw new Exception("Id is not valid");
            return _documentRepository.DeleteDocument(id);
        }

        public bool EditDocument(int id, DocumentDto documentDto)
        {
            if (id == 0) throw new Exception("Id is not valid");

            var document = _documentRepository.GetDocument(documentDto.DocumentId);
            if (document == null) return false;

            _documentRepository.Update(documentDto);
            return true;
        }

    }
}
