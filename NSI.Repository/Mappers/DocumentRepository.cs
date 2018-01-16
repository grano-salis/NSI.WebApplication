using System.IO;
using System.Linq;
using IkarusEntities;
using NSI.DC.DocumentRepository;

namespace NSI.Repository.Mappers
{
    public static class DocumentRepository
    {
        public static Document MapToDbEntity(DocumentDto document, IkarusContext _dbContext)
        {
            var doc = new Document()
            {
                CaseId = document.DocumentId,
                DocumentId = document.DocumentId,
                DocumentCategoryId = document.CategoryId,
                DocumentContent = document.DocumentContent,
                DocumentPath = document.DocumentPath,
                DocumentCategory = _dbContext.DocumentCategory.FirstOrDefault(d => d.DocumentCategoryId == document.CategoryId),
                Description = document.DocumentDescription,
                Case = _dbContext.CaseInfo.FirstOrDefault(c => c.CaseId == document.CaseId),
                DocumentHistory = _dbContext.DocumentHistory.ToList(),
                CreatedByUser = _dbContext.UserInfo.FirstOrDefault(),
                CreatedByUserId = _dbContext.UserInfo.FirstOrDefault().UserId
            };
            var extension = Path.GetExtension(doc.DocumentPath).Replace(".", "");
            if (extension != null) doc.FileType = _dbContext.FileType.FirstOrDefault(f => f.FileTypeId == document.FileTypeId);

            return doc;
        }

        public static Document MapToDbEntity(CreateDocumentDto document, IkarusContext _dbContext)
        {
            var doc = new Document()
            {
                CaseId = document.DocumentId,
                DocumentId = document.DocumentId,
                DocumentCategoryId = document.CategoryId,
                DocumentContent = document.DocumentContent,
                DocumentPath = document.DocumentPath,
                DocumentCategory = _dbContext.DocumentCategory.FirstOrDefault(d => d.DocumentCategoryId == document.CategoryId),
                Description = document.DocumentDescription,
                Case = _dbContext.CaseInfo.FirstOrDefault(c => c.CaseId == document.CaseId),
                DocumentHistory = _dbContext.DocumentHistory.Where(d=> d.DocumentId == document.DocumentId).ToList(),
                CreatedByUser = _dbContext.UserInfo.FirstOrDefault(),
                CreatedByUserId = _dbContext.UserInfo.FirstOrDefault().UserId,
                Title = document.DocumentTitle
            };
            var extension = Path.GetExtension(doc.DocumentPath).Replace(".", "");
            if (extension != null) doc.FileType = _dbContext.FileType.FirstOrDefault(f => f.Extension == extension);

            return doc;
        }

        public static DocumentDto MapToDto(Document document, IkarusContext dbContext)
        {
            var history = document.DocumentHistory.OrderBy(d => d.ModifiedAt).Select(doc => doc.ModifiedAt).ToList();
            var documentDto = new DocumentDto()
            {
                DocumentId = document.DocumentId,
                CaseId = document.CaseId,
                CategoryId = document.DocumentCategoryId,
                CategoryName = "Category title",
                LastModified = history.LastOrDefault(),
                CreatedAt = history.FirstOrDefault(),
                DocumentContent = document.DocumentContent,
                DocumentDescription = document.Description,
                DocumentPath = document.DocumentPath,
                DocumentTitle = document.Title
            };
            return documentDto;
        }


        public static DocumentDetails MapToDocumentDetailsDto(Document document, IkarusContext dbContext)
        {
            var history = document.DocumentHistory.OrderBy(d => d.ModifiedAt).Select(doc => doc.ModifiedAt).ToList();
            var documentDetails = new DocumentDetails()
            {
                DocumentId = document.DocumentId,
                DocumentTitle = document.Title,
                CaseId = document.CaseId,
                CategoryId = document.DocumentCategoryId,
                DocumentContent = document.DocumentContent,
                DocumentDescription = document.Description,
                DocumentPath = document.DocumentPath,
                FileTypeId = document.FileTypeId,
                CaseNumber = document.Case.CaseNumber,
                DocumentCategoryName = document.DocumentCategory.DocumentCategoryTitle,
                FileIconPath = document.FileType.IconPath,
                ModifiedAt = history.LastOrDefault(),
                CreatedAt = history.FirstOrDefault(),
                CreatedByUserId = 1,
                Author = "John Doe",
                
            };
            return documentDetails;

        }

        public static DocumentHistoryDto MapToDocumentHistoryDto(DocumentHistory documentHistory, IkarusContext _dbContext, int id)
        {
            var documentHistoryDto =  new DocumentHistoryDto()
            {
                ModifiedAt = documentHistory.ModifiedAt,
                DocumentTitle = documentHistory.DocumentTitle,
                Author = documentHistory.ModifiedByUser.FirstName + " " + documentHistory.ModifiedByUser.LastName,
                CaseNumber = documentHistory.CaseNumber,
                DocumentCategoryName = documentHistory.DocumentCategoryName,
                DocumentDescription = documentHistory.DocumentDescription,
                DocumentPath = documentHistory.DocumentPath,
            };
                var extension = Path.GetExtension(documentHistoryDto.DocumentPath).Replace(".", "");
                if (extension != null) documentHistoryDto.IconPath = _dbContext.FileType.FirstOrDefault(c => c.Extension == extension).IconPath;

            return documentHistoryDto;
        }

        public static DocumentCategoryNamesDto MapToDocumentCategoryNamesDto(DocumentCategory documentCategory)
        {
            return new DocumentCategoryNamesDto()
            {
                Id = documentCategory.DocumentCategoryId,
                Name = documentCategory.DocumentCategoryTitle
            };
        }

        public static DocumentDetails MapToDbEntity(Document document)
        {
            var history = document.DocumentHistory.OrderBy(d => d.ModifiedAt).Select(doc => doc.ModifiedAt).ToList();
            var documentDetails = new DocumentDetails()
            {
                DocumentId = document.DocumentId,
                DocumentTitle = document.Title,
                CaseId = document.CaseId,
                CategoryId = document.DocumentCategoryId,
                DocumentContent = document.DocumentContent,
                DocumentDescription = document.Description,
                DocumentPath = document.DocumentPath,
                FileTypeId = document.FileTypeId,
                CaseNumber = document.Case.CaseNumber,
                DocumentCategoryName = document.DocumentCategory.DocumentCategoryTitle,
                FileIconPath = document.FileType.IconPath,
                ModifiedAt = history.LastOrDefault(),
                CreatedAt = history.FirstOrDefault(),
                CreatedByUserId = 1,
                Author = "John Doe",
                
            };
            return documentDetails;
        }
    }
}
