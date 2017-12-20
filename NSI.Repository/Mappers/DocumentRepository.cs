using System;
using System.Linq;
using IkarusEntities;
using NSI.DC.DocumentRepository;

namespace NSI.Repository.Mappers
{
    public class DocumentRepository
    {
        public static Document MapToDbEntity(DocumentDto document, IkarusContext _dbContext)
        {
            return new Document()
            {
                CaseId = document.DocumentId,
                DocumentId = document.DocumentId,
                DocumentCategoryId = document.CategoryId,
                DocumentContent = document.DocumentContent,
                DocumentPath = document.DocumentPath,
                DocumentCategory = _dbContext.DocumentCategory.FirstOrDefault(d => d.DocumentCategoryId == document.CategoryId),
                Description = document.DocumentDescription,
                Case = _dbContext.CaseInfo.FirstOrDefault(c => c.CaseId == document.CaseId),
                FileType = _dbContext.FileType.FirstOrDefault(f => f.FileTypeId == document.FileTypeId),
                FileTypeId = document.FileTypeId,
                DocumentHistory = _dbContext.DocumentHistory.ToList(),
                CreatedByUser = _dbContext.UserInfo.FirstOrDefault(),
                CreatedByUserId = _dbContext.UserInfo.FirstOrDefault().UserId
            };
        }

        public static DocumentDto MapToDto(Document document, IkarusContext dbContext)
        {
            //fix
            return new DocumentDto()
            {
                DocumentId = document.DocumentId,
                CaseId = document.CaseId,
                CategoryId = document.DocumentCategoryId,
                CategoryName = "Category title",
                LastModified = DateTime.UtcNow,
                DocumentContent = document.DocumentContent,
                DocumentDescription = document.Description,
                DocumentPath = document.DocumentPath,
            };
        }


        public static DocumentDetails MapToDocumentDetailsDto(Document document, IkarusContext dbContext)
        {
            var history = document.DocumentHistory.OrderBy(d => d.ModifiedAt).ToList();
            var documentDetails = new DocumentDetails()
            {
                DocumentId = document.DocumentId,
                //DocumentTitle = document.
                CaseId = document.CaseId,
                CategoryId = document.DocumentCategoryId,
                DocumentContent = document.DocumentContent,
                DocumentDescription = document.Description,
                DocumentPath = document.DocumentPath,
                FileTypeId = document.FileTypeId,
                CaseNumber = document.Case.CaseNumber,
                DocumentCategoryName = document.DocumentCategory.CategoryTitle,
                FileIconPath = "",
                //CreatedByUserId = document.CreatedByUser.UserId,
                Author = document.CreatedByUser?.FirstName + ' ' + document.CreatedByUser?.LastName,
                
            };
            if (history.Count <= 0) return documentDetails;
            documentDetails.CreatedAt = history.LastOrDefault().ModifiedAt;
            documentDetails.ModifiedAt = history.FirstOrDefault().ModifiedAt;
            return documentDetails;

        }
    }
}
