using NSI.DC.AdminRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IAdminRepository
    {
        ICollection<CaseCategoryDto> GetCaseCategories();
        CaseCategoryDto CreateCaseCategory(CaseCategoryDto model);
        CaseCategoryDto GetCaseCategoryById(int caseCategoryId);
        bool DeleteCaseCategoryById(int caseCategoryId);
        bool EditCaseCategory(int caseCategoryId, CaseCategoryDto caseCategory);

        ICollection<ClientTypeDto> GetClientTypes();
        ClientTypeDto CreateClientType(ClientTypeDto model);
        ClientTypeDto GetClientTypeById(int clientTypeId);
        bool DeleteClientTypeById(int clientTypeId);
        bool EditClientType(int clientTypeId, ClientTypeDto clientType);

        ICollection<DocumentCategoryDto> GetDocumentCategories();
        DocumentCategoryDto CreateDocumentCategory(DocumentCategoryDto model);
        DocumentCategoryDto GetDocumentCategoryById(int documentCategoryId);
        bool DeleteDocumentCategoryById(int documentCategoryId);
        bool EditDocumentCategory(int documentCategoryId, DocumentCategoryDto documentCategory);

        ICollection<FileTypeDto> GetFileTypes();
        FileTypeDto CreateFileType(FileTypeDto model);
        FileTypeDto GetFileTypeById(int fileTypeId);
        bool DeleteFileTypeById(int fileTypeId);
        bool EditFileType(int fileTypeId, FileTypeDto fileType);


    }
}

