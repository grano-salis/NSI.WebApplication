using NSI.DC.AdminRepository;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IAdminManipulation
    {
        ICollection<CaseCategoryDto> GetCaseCategories();
        CaseCategoryDto CreateCaseCategory(CaseCategoryDto model);
        CaseCategoryDto GetCaseCategoryById(int caseCategoryId);
        bool DeleteCaseCategoryById(int caseCategoryId);
        bool EditCaseCategory(int caseCategoryId, CaseCategoryDto caseCategory);

        ICollection<ClientTypeDto> GetClientType();
        ClientTypeDto CreateClientType(CaseCategoryDto model);
        ClientTypeDto GetCaseClientTypeById(int clientTypeId);
        bool DeleteClientTypeById(int clientTypeId);
        bool EditClientType(int clientTypeId, ClientTypeDto clientType);

        ICollection<DocumentCategoryDto> GetDocumentCategory();
        DocumentCategoryDto CreateDocumentCategory(DocumentCategoryDto model);
        DocumentCategoryDto GetDocumentCategoryById(int documentCategoryId);
        bool DeleteDocumentCategoryById(int documentCategoryId);
        bool EditDocumentCategory(int documentCategoryId, DocumentCategoryDto documentCategory);

        ICollection<FileTypeDto> GetFileType();
        FileTypeDto CreateFileType(FileTypeDto model);
        FileTypeDto GetFileTypeById(int fileTypeId);
        bool DeleteFileTypeById(int fileTypeId);
        bool EditFileType(int fileTypeId, FileTypeDto fileType);


    }
}
