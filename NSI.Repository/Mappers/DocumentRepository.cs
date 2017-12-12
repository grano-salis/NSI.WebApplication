using IkarusEntities;
using NSI.DC.DocumentRepository;

namespace NSI.Repository.Mappers
{
    public class DocumentRepository
    {
        public static Document MapToDbEntity(DocumentDto document)
        {
            return new Document()
            {
                CaseId = document.DocumentId,
                DocumentId = document.DocumentId
            };
        }

        public static DocumentDto MapToDto(Document document)
        {
            return new DocumentDto()
            {
                DocumentId = document.DocumentId
            };
        }

        
    }
}
