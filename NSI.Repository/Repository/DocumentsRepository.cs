using NSI.Repository.Interfaces;
using System;
using NSI.DC.DocumentRepository;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IkarusEntities;

namespace NSI.Repository
{
    public partial class DocumentsRepository : IDocumentRepository
    {
        private readonly IkarusContext _dbContext;

        public DocumentsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        REST.Models.DocumentsPagingResultModel IDocumentRepository.GetAllDocuments(REST.Models.DocumentsPagingQueryModel query)
        {
            var result = new REST.Models.DocumentsPagingResultModel();
            result.ItemsPerPage = 10;
            var documents = _dbContext.Document.Where(doc => typeof(Document).GetProperty(query.FilterBy).GetValue(doc, null).ToString().Contains(query.Search)).Select(d => DocumentRepository.MapToDto(d));
            result.TotalItems = documents.Count();
            result.Results = documents.Take(result.ItemsPerPage).ToList();
            return result;
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
        public int GetDocumentsByCase(int caseId)
        {
            var documents = _dbContext.Document.Where(x => x.CaseId == caseId);
            int n = documents.Count();
            return n;


        }
    }
}
