﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return "File not selected";

            var guid = Guid.NewGuid().ToString().Substring(0,7);
            List<string> arrayFileName = new List<string>(file.FileName.Split('.'));
            int lastIndex = arrayFileName.Count - 1;
            string extension = arrayFileName[lastIndex];
            arrayFileName.RemoveAt(lastIndex);
            arrayFileName.Add("-" + guid + "." + extension);

            string rightPath = Path.Combine("Documents", String.Join("", arrayFileName));
            var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", rightPath);
            new FileInfo(path).Directory.Create();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return rightPath;
        }

        public bool SaveDocument(CreateDocumentDto document)
        {
            if (document == null)
            {
                throw new ArgumentException("Document is not valid.");
            }

            var result = _documentRepository.SaveDocument(document);
            return result != 0;
        }

        public List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id is not valid");
            }
            return _documentRepository.GetDocumentHistoryByDocumentId(id);
        }

        public List<DocumentDto> GetDocumentsByCase(int id)
        {
            return _documentRepository.GetDocumentsByCase(id);
        }
        public int GetNumberOfDocumentsByCase(int caseId)
        {      
            return _documentRepository.GetNumberOfDocumentsByCase(caseId);
        }

        public PagingResultModel<DocumentDetails> GetDocumentsByPage(DocumentsPagingQueryModel query)
        {
            if (query.PageNumber < 0)
            {
                throw new ArgumentException("Page number is not valid");
            }
            return _documentRepository.GetAllDocumentsByPage(query);
        }

        public DocumentDetails GetDocumentById(int documentId)
        {
            if (documentId == 0)
            {
                throw new ArgumentException("Id is not valid.");
            }

            return _documentRepository.GetDocument(documentId);
        }

        public bool DeleteDocument(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id is not valid.");
            }

            return _documentRepository.DeleteDocument(id);
        }

        public bool EditDocument(int id, DocumentDto documentDto)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id is not valid.");
            }

            var document = _documentRepository.GetDocument(documentDto.DocumentId);

            if (document == null)
            {
                return false;
            }

            _documentRepository.Update(documentDto);
            return true;
        }


    }
}
