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
