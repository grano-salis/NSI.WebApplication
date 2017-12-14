using System;
using System.Collections.Generic;
using System.Linq;
using IkarusEntities;
using NSI.DC.DocumentRepository;
using NSI.Repository.Interfaces;
using NSI.REST.Models;

namespace NSI.Repository.Repository
{
    public class DocumentsRepository : IDocumentRepository
    {
        private readonly IkarusContext _dbContext;

        public DocumentsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        PagingResultModel<DocumentDto> IDocumentRepository.GetAllDocuments(DocumentsPagingQueryModel query)
        {
            var result = new PagingResultModel<DocumentDto>
            {
                ItemsPerPage = 10
            };
            var documents = _dbContext.Document.Where(doc => typeof(Document).GetProperty(query.FilterBy).GetValue(doc, null).ToString().Contains(query.Search)).Select(d => DocumentRepository.MapToDto(d));
            result.TotalItems = documents.Count();
            result.Results = documents.Take(result.ItemsPerPage).ToList();
            return result;
        }

        public bool DeleteDocument(int id)
        {
            var document = _dbContext.Document.FirstOrDefault(d => d.CaseId == id);
            //document.isDeleted = true;
            var response = _dbContext.Update(document);
            //implement deleting history
            return response != null;
        }

        public void Update(DocumentDto document)
        {
            _dbContext.Update(document);
            //go to history and add record
        }


        DocumentDto IDocumentRepository.GetDocument(int documentId)
        {
            Document document =  _dbContext.Document.FirstOrDefault(x => x.DocumentId == documentId);

            return document != null ? DocumentRepository.MapToDto(document) : null;
        }

        long IDocumentRepository.SaveDocument(DocumentDto document)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DocumentDto> IDocumentRepository.SearchDocuments(DocumentSearchCriteriaDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            return null;
        }
    }
}
