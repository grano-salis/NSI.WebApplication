using System;
using System.Collections.Generic;
using System.Linq;
using IkarusEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSI.DC.DocumentRepository;
using NSI.Repository.Interfaces;
using NSI.Repository.Mappers;
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

        public List<DocumentDto> GetAllDocuments()
        {
            return _dbContext.Document.Select(document => DocumentRepository.MapToDto(document, _dbContext)).ToList();
        }

        PagingResultModel<DocumentDto> IDocumentRepository.GetAllDocumentsByPage(DocumentsPagingQueryModel query)
        {
            var result = new PagingResultModel<DocumentDto>
            {
                ItemsPerPage = 10
            };
            var documents = _dbContext.Document.Where(doc => SearchByMultipleProperties(query, doc)).Select(d => DocumentRepository.MapToDto(d, _dbContext));
            result.TotalItems = documents.Count();
            result.Results = documents.Take(result.ItemsPerPage).ToList();
            return result;
        }

        private static bool SearchByMultipleProperties(DocumentsPagingQueryModel query, Document doc)
        {
            //add all properties
            var isTrue = doc.Description.Contains(query.SearchByTitle) && doc.CaseId.Equals(query.SearchByCaseId);
            if (query.SearchByDateFrom != null)
            {
                var dateCreated = doc.DocumentHistory.Where(d => d.DocumentId == doc.DocumentId).OrderBy(document => document.ModifiedAt).FirstOrDefault();
                if (dateCreated != null && dateCreated.ModifiedAt.Date != query.SearchByDateFrom.Date) return false;
            }
            return isTrue;
        }

        public bool DeleteDocument(int id)
        {
            var document = _dbContext.Document.FirstOrDefault(d => d.CaseId == id);
            //document.isDeleted = true;
            var response = _dbContext.Update(document);
            AddToHistory(document);
            return response != null;
        }

        public int Update(DocumentDto document)
        {
            var documentEntity = _dbContext.Document.FirstOrDefault(d => d.DocumentId == document.DocumentId);

            documentEntity.DocumentId = document.DocumentId;
            documentEntity.CaseId = document.CaseId;
            documentEntity.Case = _dbContext.CaseInfo.FirstOrDefault(c => c.CaseId == document.CategoryId);
            documentEntity.DocumentCategory =
                _dbContext.DocumentCategory.FirstOrDefault(c => c.DocumentCategoryId == document.CategoryId);
            documentEntity.DocumentContent = document.DocumentContent;
            documentEntity.DocumentPath = document.DocumentPath;
            documentEntity.FileType = _dbContext.FileType.FirstOrDefault(c => c.FileTypeId == document.FileTypeExtension);
            documentEntity.FileTypeId = document.FileTypeExtension;
            documentEntity.DocumentContent = document.DocumentContent;
            documentEntity.Description = document.DocumentDescription;
            documentEntity.DocumentPath = document.DocumentPath;

            _dbContext.Update(documentEntity);
            var result = _dbContext.SaveChanges();

            //AddToHistory(documentEntity);
            return result;
        }

        private void AddToHistory(Document document)
        {
            //TODO get current user
            var documentHistoryRecord = new DocumentHistory()
            {
                Document = document,
                DocumentId = document.DocumentId,
                ModifiedAt = DateTime.UtcNow,
                ModifiedByUser = _dbContext.UserInfo.FirstOrDefault(u => u.UserId == 1),
                ModifiedByUserId = 1
            };
            _dbContext.DocumentHistory.Add(documentHistoryRecord);
            _dbContext.SaveChanges();
        }


        DocumentDto IDocumentRepository.GetDocument(int documentId)
        {
            Document document = _dbContext.Document.FirstOrDefault(x => x.DocumentId == documentId);

            return document != null ? DocumentRepository.MapToDto(document, _dbContext) : null;
        }

        int IDocumentRepository.SaveDocument(DocumentDto document)
        {
            try
            {
                var documentEntity = DocumentRepository.MapToDbEntity(document, _dbContext);
                _dbContext.Add(documentEntity);
                var result = _dbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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