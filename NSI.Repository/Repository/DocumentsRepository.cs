﻿using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IkarusEntities;
using Microsoft.EntityFrameworkCore;
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

        public List<DocumentDetails> GetAllDocuments()
        {
            var documents = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory).Include(h=>h.DocumentHistory).Include(f => f.FileType);
            return documents.Where(doc => !doc.IsDeleted).Select(document => DocumentRepository.MapToDocumentDetailsDto(document, _dbContext)).ToList();
        }

        public List<DocumentHistoryDto> GetDocumentHistoryByDocumentId(int id)
        {
            return _dbContext.DocumentHistory.Include(x => x.ModifiedByUser).Include(d => d.Document).Include(d => d.Document.Case).Include(d => d.Document.DocumentCategory).Where(d => d.DocumentId == id).Select(h=>DocumentRepository.MapToDocumentHistoryDto(h)).ToList();
        }

        public List<DocumentDto> GetDocumentsByCase(int id)
        {
            var documents = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory);
            return documents.Where(doc => !doc.IsDeleted && doc.CaseId == id).Select(document => DocumentRepository.MapToDto(document, _dbContext)).ToList();
        }
        public int GetNumberOfDocumentsByCase(int caseId)
        {
            var documents = _dbContext.Document.Where(x => x.CaseId == caseId);
            int n = documents.Count();
            return n;


        }
        PagingResultModel<DocumentDetails> IDocumentRepository.GetAllDocumentsByPage(DocumentsPagingQueryModel query)
        {
            var result = new PagingResultModel<DocumentDetails>
            {
                ItemsPerPage = query.ResultsPerPage
            };
            var documents = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory).Include(h => h.DocumentHistory).Include(f=>f.FileType).ToList();
            var filteredDocuments = documents.Where(doc => SearchByMultipleProperties(query, doc))
                .Select(d => DocumentRepository.MapToDocumentDetailsDto(d, _dbContext));
            result.TotalItems = filteredDocuments.Count();
            result.Results = filteredDocuments.Skip(query.ResultsPerPage*(query.PageNumber-1)).Take(query.ResultsPerPage).ToList();
            return result;
        }

        private static bool SearchByMultipleProperties(DocumentsPagingQueryModel query, Document doc)
        {
            if (doc.IsDeleted) return false;
            var documentHistory = doc.DocumentHistory.Where(d => d.DocumentId == doc.DocumentId).OrderBy(document => document.ModifiedAt).ToList();
            if (query.CreatedDateFrom?.Date != null && documentHistory.FirstOrDefault()?.ModifiedAt.Date < query.CreatedDateFrom?.Date) return false;
            if (query.CreatedDateFrom?.Date != null && documentHistory.FirstOrDefault()?.ModifiedAt.Date > query.CreatedDateFrom?.Date) return false;
            if (query.CreatedDateTo?.Date != null && documentHistory.LastOrDefault()?.ModifiedAt.Date < query.CreatedDateTo?.Date) return false;
            if (query.CreatedDateTo?.Date != null && documentHistory.LastOrDefault()?.ModifiedAt.Date > query.CreatedDateTo?.Date) return false;
            if (query.SearchByCategoryId != 0 && doc.DocumentCategoryId != query.SearchByCategoryId) return false;
            if(query.SearchByCaseId != 0 && doc.CaseId != query.SearchByCaseId) return false;
            if(query.SearchByTitle != "" && !doc.Title.Contains(query.SearchByTitle)) return false;
            return query.SearchByDescription == "" || !doc.Description.Contains(query.SearchByDescription);
        }

        public bool DeleteDocument(int id)
        {
            var document = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory).FirstOrDefault(d => d.DocumentId == id);
            document.IsDeleted = true;
            var response = _dbContext.Update(document);
            AddToHistory(document);
            _dbContext.SaveChanges();
            return response != null;
        }

        public int Update(DocumentDto document)
        {
            var documentEntity = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory).FirstOrDefault(d => d.DocumentId == document.DocumentId);

            documentEntity.DocumentId = document.DocumentId;
            documentEntity.CaseId = document.CaseId;
            documentEntity.Case = _dbContext.CaseInfo.FirstOrDefault(c => c.CaseId == document.CaseId);
            documentEntity.DocumentCategory =
                _dbContext.DocumentCategory.FirstOrDefault(c => c.DocumentCategoryId == document.CategoryId);
            documentEntity.DocumentContent = document.DocumentContent;
            documentEntity.DocumentPath = document.DocumentPath;
            //documentEntity.FileType =
            //    _dbContext.FileType.FirstOrDefault(c => c.FileTypeId == document.FileTypeId);
            //documentEntity.FileTypeId = document.FileTypeId;
            documentEntity.DocumentContent = document.DocumentContent;
            documentEntity.Description = document.DocumentDescription;
            documentEntity.DocumentPath = document.DocumentPath;
            documentEntity.Title = document.DocumentTitle;
            if (documentEntity.DocumentPath != null)
                documentEntity.FileTypeId = _dbContext.FileType
                    .FirstOrDefault(c => c.Extension == Path.GetExtension(document.DocumentPath).Replace(".", ""))
                    .FileTypeId;
            else documentEntity.FileTypeId = _dbContext.Document.Find(documentEntity.DocumentId).FileTypeId;
            _dbContext.Update(documentEntity);
            var result = _dbContext.SaveChanges();

            AddToHistory(documentEntity);
            return result;
        }

        private void AddToHistory(Document document)
        {
            var documentHistoryRecord = new DocumentHistory()
            {
                DocumentHistoryId = 0,
                Document = document,
                DocumentId = document.DocumentId,
                ModifiedAt = DateTime.UtcNow,
                ModifiedByUser = _dbContext.UserInfo.FirstOrDefault(u => u.UserId == 1),
                ModifiedByUserId = 1,
                DocumentPath = document.DocumentPath,
                CaseNumber = document.Case.CaseNumber,
                DocumentCategoryName = document.DocumentCategory.DocumentCategoryTitle,
                DocumentDescription = document.Description,
                DocumentTitle = document.Title
            };
            _dbContext.DocumentHistory.Add(documentHistoryRecord);
            _dbContext.SaveChanges();
        }


        DocumentDetails IDocumentRepository.GetDocument(int documentId)
        {
            var document = _dbContext.Document.Include(x => x.Case).Include(x => x.DocumentCategory).Include(h=>h.DocumentHistory).Include(f=>f.FileType).FirstOrDefault(x => x.DocumentId == documentId);
            return document != null ? DocumentRepository.MapToDocumentDetailsDto(document, _dbContext) : null;
        }

        int IDocumentRepository.SaveDocument(CreateDocumentDto document)
        {
            try
            {
                document.DocumentId = 0;
                var documentEntity = DocumentRepository.MapToDbEntity(document, _dbContext);
                _dbContext.Add(documentEntity);
                var result = _dbContext.SaveChanges();
                AddToHistory(documentEntity);
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
}