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
                FileType = _dbContext.FileType.FirstOrDefault(f => f.FileTypeId == document.FileTypeExtension)
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

        
    }
}
