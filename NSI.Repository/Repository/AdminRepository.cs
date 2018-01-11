using IkarusEntities;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSI.DC.AdminRepository;
using System.Collections;

namespace NSI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IkarusContext _dbContext;

        public AdminRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Case Category

        public ICollection<CaseCategoryDto> GetCaseCategories()
        {
            try
            {
                var caseCategories = _dbContext.CaseCategory.Where(x => x.IsDeleted == false);
                if (caseCategories != null)
                {
                    ICollection<CaseCategoryDto> caseCategoryDto = new List<CaseCategoryDto>();
                    foreach (var item in caseCategories)
                    {
                        caseCategoryDto.Add(Mappers.AdminRepository.MapToDto(item));
                    }
                    return caseCategoryDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get case categories!");
            }
            return new List<CaseCategoryDto>();
        }

        public CaseCategoryDto CreateCaseCategory(CaseCategoryDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Model is null!");
            }

            try
            {
                var caseCategory = Mappers.AdminRepository.MapToDbEntity(model);
                caseCategory.DateModified = caseCategory.DateCreated = DateTime.Now;
                caseCategory.IsDeleted = false;
                _dbContext.Add(caseCategory);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AdminRepository.MapToDto(caseCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot create new case category!");
            }
            return null;
        }

        public bool DeleteCaseCategoryById(int caseCategoryId)
        {
            try
            {
                var caseCategory = _dbContext.CaseCategory.FirstOrDefault(x => x.CaseCategoryId == caseCategoryId);
                if (caseCategory != null)
                {
                    caseCategory.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot delete case category!");
            }
        }

        public CaseCategoryDto GetCaseCategoryById(int caseCategoryId)
        {
            try
            {
                var caseCategory = _dbContext.CaseCategory.FirstOrDefault(x => x.CaseCategoryId == caseCategoryId && x.IsDeleted == false);
                if (caseCategory != null)
                {
                    return Mappers.AdminRepository.MapToDto(caseCategory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get case category!");
            }
            return null;
        }

        public bool EditCaseCategory(int caseCategoryId, CaseCategoryDto model)
        {
            try
            {
                var caseCategory = _dbContext.CaseCategory.FirstOrDefault(x => x.CaseCategoryId == caseCategoryId);
                if (caseCategory != null)
                {
                    caseCategory.CaseCategoryName = model.CaseCategoryName;
                    caseCategory.CustomerId = model.CustomerId;
                    caseCategory.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot update case category!");
            }
        }


        //Client Type

        public ICollection<ClientTypeDto> GetClientTypes()
        {
            try
            {
                var clientTypes = _dbContext.ClientType.Where(x => x.IsDeleted == false);
                if (clientTypes != null)
                {
                    ICollection<ClientTypeDto> clientTypeDto = new List<ClientTypeDto>();
                    foreach (var item in clientTypes)
                    {
                        clientTypeDto.Add(Mappers.AdminRepository.MapToDtoClient(item));
                    }
                    return clientTypeDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get client types!");
            }
            return new List<ClientTypeDto>();
        }

        public ClientTypeDto CreateClientType(ClientTypeDto model)
        {
            try
            {
                var clientType = Mappers.AdminRepository.MapToDbEntityClient(model);
                clientType.DateModified = clientType.DateCreated = DateTime.Now;
                clientType.IsDeleted = false;
                _dbContext.Add(clientType);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AdminRepository.MapToDtoClient(clientType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot create new client type!");
            }
            return null;
        }

        public bool DeleteClientTypeById(int clientTypeId)
        {
            try
            {
                var clientType = _dbContext.ClientType.FirstOrDefault(x => x.ClientTypeId == clientTypeId);
                if (clientType != null)
                {
                    clientType.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot delete client type!");
            }
        }

        public ClientTypeDto GetClientTypeById(int clientTypeId)
        {
            try
            {
                var clientType = _dbContext.ClientType.FirstOrDefault(x => x.ClientTypeId == clientTypeId && x.IsDeleted == false);
                if (clientType != null)
                {
                    return Mappers.AdminRepository.MapToDtoClient(clientType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get client type!");
            }
            return null;
        }

        public bool EditClientType(int clientTypeId, ClientTypeDto model)
        {
            try
            {
                var clientType = _dbContext.ClientType.FirstOrDefault(x => x.ClientTypeId == clientTypeId);
                if (clientType != null)
                {
                    clientType.ClientTypeName = model.ClientTypeName;
                    clientType.CustomerId = model.CustomerId;
                    clientType.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot update client type!");
            }
        }


        //Document Category
       
        public ICollection<DocumentCategoryDto> GetDocumentCategories()
        {
            try
            {
                var documentCategories = _dbContext.DocumentCategory.Where(x => x.IsDeleted == false);
                if (documentCategories != null)
                {
                    ICollection<DocumentCategoryDto> documentCategoryDto = new List<DocumentCategoryDto>();
                    foreach (var item in documentCategories)
                    {
                        documentCategoryDto.Add(Mappers.AdminRepository.MapToDtoDocument(item));
                    }
                    return documentCategoryDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get document categories!");
            }
            return new List<DocumentCategoryDto>();
        }

        public DocumentCategoryDto CreateDocumentCategory(DocumentCategoryDto model)
        {
            try
            {
                var documentCategory = Mappers.AdminRepository.MapToDbEntityDocument(model);
                documentCategory.DateModified = documentCategory.DateCreated = DateTime.Now;
                documentCategory.IsDeleted = false;
                _dbContext.Add(documentCategory);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AdminRepository.MapToDtoDocument(documentCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot create new document category!");
            }
            return null;
        }

        public bool DeleteDocumentCategoryById(int documentCategoryId)
        {
            try
            {
                var docuemntCategory = _dbContext.DocumentCategory.FirstOrDefault(x => x.DocumentCategoryId == documentCategoryId);
                if (docuemntCategory != null)
                {
                    docuemntCategory.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot delete document category!");
            }
        }

        public DocumentCategoryDto GetDocumentCategoryById(int documentCategoryId)
        {
            try
            {
                var documentCategory = _dbContext.DocumentCategory.FirstOrDefault(x => x.DocumentCategoryId == documentCategoryId && x.IsDeleted == false);
                if (documentCategory != null)
                {
                    return Mappers.AdminRepository.MapToDtoDocument(documentCategory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get document category!");
            }
            return null;
        }

        public bool EditDocumentCategory(int documentCategoryId, DocumentCategoryDto model)
        {
            try
            {
                var documentCategory = _dbContext.DocumentCategory.FirstOrDefault(x => x.DocumentCategoryId == documentCategoryId);
                if (documentCategory != null)
                {
                    documentCategory.DocumentCategoryTitle = model.DocumentCategoryTitle;
                    documentCategory.CustomerId = model.CustomerId;
                    documentCategory.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot update document category!");
            }
        }

        //File Type

        public ICollection<FileTypeDto> GetFileTypes()
        {
            try
            {
                var fileTypes = _dbContext.FileType.Where(x => x.IsDeleted == false);
                if (fileTypes != null)
                {
                    ICollection<FileTypeDto> fileTypeDto = new List<FileTypeDto>();
                    foreach (var item in fileTypes)
                    {
                        fileTypeDto.Add(Mappers.AdminRepository.MapToDtoFile(item));
                    }
                    return fileTypeDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get file types!");
            }
            return new List<FileTypeDto>();
        }

        public FileTypeDto CreateFileType(FileTypeDto model)
        {
            try
            {
                var fileType = Mappers.AdminRepository.MapToDbEntityFile(model);
                fileType.DateModified = fileType.DateCreated = DateTime.Now;
                fileType.IsDeleted = false;
                _dbContext.Add(fileType);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.AdminRepository.MapToDtoFile(fileType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot create new file type!");
            }
            return null;
        }

        public bool DeleteFileTypeById(int fileTypeId)
        {
            try
            {
                var fileType = _dbContext.FileType.FirstOrDefault(x => x.FileTypeId == fileTypeId);
                if (fileType != null)
                {
                    fileType.IsDeleted = true;
                    _dbContext.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot delete file type!");
            }
        }

        public FileTypeDto GetFileTypeById(int fileTypeId)
        {
            try
            {
                var fileType = _dbContext.FileType.FirstOrDefault(x => x.FileTypeId == fileTypeId && x.IsDeleted == false);
                if (fileType != null)
                {
                    return Mappers.AdminRepository.MapToDtoFile(fileType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot get file type!");
            }
            return null;
        }

        public bool EditFileType(int fileTypeId, FileTypeDto model)
        {
            try
            {
                var fileType = _dbContext.FileType.FirstOrDefault(x => x.FileTypeId == fileTypeId);
                if (fileType != null)
                {
                    fileType.Extension = model.Extension;
                    fileType.IconPath = model.IconPath;
                    fileType.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception("Cannot update file type!");
            }
        }   
    }
}
