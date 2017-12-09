using IkarusEntities;
using NSI.DC.AdminRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    class AdminRepository
    {
        //Case Category

        public static CaseCategory MapToDbEntity(CaseCategoryDto caseCategoryDto)
        {
            return new CaseCategory()
            {
                CaseCategoryId = caseCategoryDto.CaseCategoryId,
                CaseCategoryName = caseCategoryDto.CaseCategoryName,
                DateCreated = caseCategoryDto.DateCreated,
                DateModified = caseCategoryDto.DateModified,
                IsDeleted = caseCategoryDto.IsDeleted,
                CustomerId = caseCategoryDto.CustomerId
            };
        }

        public static CaseCategoryDto MapToDto(CaseCategory caseCategory)
        {
            return new CaseCategoryDto()
            {
                CaseCategoryId = caseCategory.CaseCategoryId,
                CaseCategoryName = caseCategory.CaseCategoryName,
                DateCreated = caseCategory.DateCreated,
                DateModified = caseCategory.DateModified,
                IsDeleted = caseCategory.IsDeleted,
                CustomerId = caseCategory.CustomerId
            };
        }

        //Client Type

        public static ClientType MapToDbEntityClient(ClientTypeDto clientTypeDto)
        {
            return new ClientType()
            {
                ClientTypeId = clientTypeDto.ClientTypeId,
                ClientTypeName = clientTypeDto.ClientTypeName,
                DateCreated = clientTypeDto.DateCreated,
                DateModified = clientTypeDto.DateModified,
                IsDeleted = clientTypeDto.IsDeleted,
                CustomerId = clientTypeDto.CustomerId
            };
        }

        public static ClientTypeDto MapToDtoClient(ClientType clientType)
        {
            return new ClientTypeDto()
            {
                ClientTypeId = clientType.ClientTypeId,
                ClientTypeName = clientType.ClientTypeName,
                DateCreated = clientType.DateCreated,
                DateModified = clientType.DateModified,
                IsDeleted = clientType.IsDeleted,
                CustomerId = clientType.CustomerId
            };
        }

        //Document Category
        public static DocumentCategory MapToDbEntityDocument(DocumentCategoryDto documentCategoryDto)
        {
            return new DocumentCategory()
            {
                DocumentCategoryId = documentCategoryDto.DocumentCategoryId,
                DocumentCategoryTitle = documentCategoryDto.DocumentCategoryTitle,
                IsDeleted = documentCategoryDto.IsDeleted,
                DateCreated = documentCategoryDto.DateCreated,
                DateModified = documentCategoryDto.DateModified,
                CustomerId = documentCategoryDto.CustomerId
            };
        }

        public static DocumentCategoryDto MapToDtoDocument(DocumentCategory documentCategory)
        {
            return new DocumentCategoryDto()
            {
                DocumentCategoryId = documentCategory.DocumentCategoryId,
                DocumentCategoryTitle = documentCategory.DocumentCategoryTitle,
                IsDeleted = documentCategory.IsDeleted,
                DateCreated = documentCategory.DateCreated,
                DateModified = documentCategory.DateModified,
                CustomerId = documentCategory.CustomerId
            };
        }


        //FileType

        public static FileType MapToDbEntityFile(FileTypeDto fileTypeDto)
        {
            return new FileType()
            {
                FileTypeId = fileTypeDto.FileTypeId,
                Extension = fileTypeDto.Extension,
                IconPath = fileTypeDto.IconPath
            };
        }

        public static FileTypeDto MapToDtoFile(FileType fileType)
        {
            return new FileTypeDto()
            {
                FileTypeId = fileType.FileTypeId,
                Extension = fileType.Extension,
                IconPath = fileType.IconPath
            };
        }



    }
}
