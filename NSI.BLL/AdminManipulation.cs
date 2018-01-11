using NSI.DC.AdminRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL
{
    public class AdminManipulation : Interfaces.IAdminManipulation
    {
        private readonly IAdminRepository _adminRepository;


        public AdminManipulation(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }


        //CaseCategory

        public ICollection<CaseCategoryDto> GetCaseCategories()
        {
            return _adminRepository.GetCaseCategories();
        }

        public CaseCategoryDto CreateCaseCategory(CaseCategoryDto model)
        {
            return _adminRepository.CreateCaseCategory(model);
        }

        public bool DeleteCaseCategorytById(int caseCategoryId)
        {
            return _adminRepository.DeleteCaseCategoryById(caseCategoryId);
        }

        public CaseCategoryDto GetCaseCategoryById(int caseCategoryId)
        {
            return _adminRepository.GetCaseCategoryById(caseCategoryId);
        }

        public bool EditCaseCategory(int caseCategoryId, CaseCategoryDto caseCategory)
        {
            return _adminRepository.EditCaseCategory(caseCategoryId, caseCategory);
        }

        //Document Category

        public ICollection<DocumentCategoryDto> GetDocumentCategories()
        {
            return _adminRepository.GetDocumentCategories();
        }

        public DocumentCategoryDto CreateDocumentCategory(DocumentCategoryDto model)
        {
            return _adminRepository.CreateDocumentCategory(model);
        }

        public bool DeleteDocumentCategorytById(int documentCategoryId)
        {
            return _adminRepository.DeleteCaseCategoryById(documentCategoryId);
        }

        public DocumentCategoryDto GetDocumentCategoryById(int documentCategoryId)
        {
            return _adminRepository.GetDocumentCategoryById(documentCategoryId);
        }

        public bool EditDocumentCategory(int documentCategoryId, DocumentCategoryDto documentCategory)
        {
            return _adminRepository.EditDocumentCategory(documentCategoryId, documentCategory);
        }

        //File Type
        public ICollection<FileTypeDto> GetFileTypes()
        {
            return _adminRepository.GetFileTypes();
        }

        public FileTypeDto CreateFileType(FileTypeDto model)
        {
            return _adminRepository.CreateFileType(model);
        }

        public bool DeleteFileTypeById(int fileTypeId)
        {
            return _adminRepository.DeleteFileTypeById(fileTypeId);
        }

        public FileTypeDto GetFileTypeById(int fileTypeId)
        {
            return _adminRepository.GetFileTypeById(fileTypeId);
        }

        public bool EditFileType(int fileTypeId, FileTypeDto fileType)
        {
            return _adminRepository.EditFileType(fileTypeId, fileType);
        }


        //CLient type

        public ICollection<ClientTypeDto> GetClientTypes()
        {
            return _adminRepository.GetClientTypes();
        }

        public ClientTypeDto CreateClientType(ClientTypeDto model)
        {
            return _adminRepository.CreateClientType(model);
        }

        public bool DeleteClientTypeById(int clientTypeId)
        {
            return _adminRepository.DeleteClientTypeById(clientTypeId);
        }

        public ClientTypeDto GetClientTypeById(int clientTypeId)
        {
            return _adminRepository.GetClientTypeById(clientTypeId);
        }

        public bool EditDocumentCategory(int clientTypeId, ClientTypeDto clientTypeDto)
        {
            return _adminRepository.EditClientType(clientTypeId, clientTypeDto);
        }

        public bool DeleteCaseCategoryById(int caseCategoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<ClientTypeDto> GetClientType()
        {
            throw new NotImplementedException();
        }

        public ClientTypeDto CreateClientType(CaseCategoryDto model)
        {
            throw new NotImplementedException();
        }

        public ClientTypeDto GetCaseClientTypeById(int clientTypeId)
        {
            throw new NotImplementedException();
        }

        public bool EditClientType(int clientTypeId, ClientTypeDto clientType)
        {
            throw new NotImplementedException();
        }

        public ICollection<DocumentCategoryDto> GetDocumentCategory()
        {
            throw new NotImplementedException();
        }

        public bool DeleteDocumentCategoryById(int documentCategoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<FileTypeDto> GetFileType()
        {
            throw new NotImplementedException();
        }
    }
}
