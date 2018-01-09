using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AdminRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests
{
    public class AdminControllerTest
    {
        IkarusContext db = new IkarusContext();
        IAdminRepository adminRepository => new AdminRepository(db);
        IAdminManipulation adminManipulation => new AdminManipulation(adminRepository);

        //Case Category
        [Fact]
        public void GetCaseCategoryByIdTest()
        {
            var controller = new AdminController(this.adminManipulation);

            var result = controller.GetCase(2);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllCasesTest()
        {

            var controller = new AdminController(this.adminManipulation);

            var caseCategories = controller.GetCases();
            Assert.IsType<OkObjectResult>(caseCategories);
        }


        [Fact]
        public void AddContactTest()
        {
            //Empty Model

            var controller = new AdminController(this.adminManipulation);

            // Empty Model state
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.PostCase(new CaseCategoryDto());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            var caseCategory = new CaseCategoryDto()
            {
                CaseCategoryId = 1,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
            };
            result = controller.PostCase(caseCategory);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Validation

            caseCategory.CaseCategoryName = "----------!!!";
            caseCategory.CustomerId = 1;
            result = controller.PostCase(caseCategory);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void AddCaseOk()
        {
            var caseCategory = new CaseCategoryDto()
            {
                CaseCategoryId = 10,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
        };
            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            mockRepo.Setup(x => x.CreateCaseCategory(It.IsAny<CaseCategoryDto>())).Returns(caseCategory);
            var result = controller.PostCase(caseCategory);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateCaseCategoryTest()
        {
            var caseCategory = new CaseCategoryDto()
            {
                CaseCategoryId = 10,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateCaseCategory(It.IsAny<CaseCategoryDto>())).Returns(caseCategory);
            var controller = new AdminController(mockRepo.Object);
            controller.PostCase(caseCategory);

            //No content Result

            caseCategory.CaseCategoryName = "NewName";
            var result = controller.PutCase(1, caseCategory);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutCase(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteCaseCategoryTest()
        {
            var caseCategory = new CaseCategoryDto()
            {
                CaseCategoryId = 10,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Invalid Model
            var result = controller.DeleteCase(10000);
            Assert.IsType<NoContentResult>(result);

            //OK
            mockRepo.Setup(x => x.CreateCaseCategory(It.IsAny<CaseCategoryDto>())).Returns(caseCategory);
            mockRepo.Setup(x => x.DeleteCaseCategoryById(It.IsAny<int>())).Returns(true);
            controller.PostCase(caseCategory);

            result = controller.DeleteCase(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //Client Type

        [Fact]
        public void GetClientTypeByIdTest()
        {
            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostClient(clientType);

            var result = controller.GetClient(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllClientsTest()
        {
            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var clientType2 = new ClientTypeDto()
            {
                ClientTypeId = 2,
                ClientTypeName = "secondClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostClient(clientType);
            controller.PostClient(clientType2);

            var clientTypes = controller.GetClients();
            Assert.IsType<OkObjectResult>(clientTypes);
        }


        [Fact]
        public void AddClientTypeTest()
        {
            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);

            // Empty Model state
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.PostClient(new ClientTypeDto());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
            };
            result = controller.PostClient(clientType);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Validation

            clientType.ClientTypeName = "......!!!";
            clientType.CustomerId = 1;
            result = controller.PostClient(clientType);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddClientTypeOk()
        {
            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType);
            var result = controller.PostClient(clientType);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateClientTypeTest()
        {
            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 10,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostClient(clientType);

            //No content Result

            clientType.ClientTypeName = "NewName";
            var result = controller.PutClient(1, clientType);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutClient(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteClientTypeTest()
        {
            var clientType = new ClientTypeDto()
            {
                ClientTypeId = 10,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Invalid Model
            var result = controller.DeleteClient(10000);
            Assert.IsType<NoContentResult>(result);

            //OK
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType);
            mockRepo.Setup(x => x.DeleteClientTypeById(It.IsAny<int>())).Returns(true);
            controller.PostClient(clientType);

            result = controller.DeleteClient(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //Document Category

        [Fact]
        public void GetDocumentCategoryByIdTest()
        {
            var documentCategory = new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory);
            var controller = new AdminController(mockRepo.Object);
            controller.PostDocument(documentCategory);

            var result = controller.GetDocument(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllDocumentsTest()
        {
            var documentCategory= new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var documentCategory2 = new DocumentCategoryDto()
            {
                DocumentCategoryId = 2,
                DocumentCategoryTitle = "secondDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory);
            var controller = new AdminController(mockRepo.Object);
            controller.PostDocument(documentCategory);
            controller.PostDocument(documentCategory2);

            var documentCategories = controller.GetDocuments();
            Assert.IsType<OkObjectResult>(documentCategories);
        }


        [Fact]
        public void AddDocumentCategoryTest()
        {
            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);

            // Empty Model state
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.PostDocument(new DocumentCategoryDto());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            var documentCategory = new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
            };
            result = controller.PostDocument(documentCategory);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Validation

            documentCategory.DocumentCategoryTitle = "......!!!";
            documentCategory.CustomerId = 1;
            result = controller.PostDocument(documentCategory);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddDocumentCategoryOk()
        {
            var documentCategory = new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory);
            var result = controller.PostDocument(documentCategory);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateDocumentCategoryTest()
        {
            var documentCategory = new DocumentCategoryDto()
            {
                DocumentCategoryId = 10,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory);
            var controller = new AdminController(mockRepo.Object);
            controller.PostDocument(documentCategory);

            //No content Result

            documentCategory.DocumentCategoryTitle = "NewName";
            var result = controller.PutDocument(1, documentCategory);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutDocument(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteDocumentCategoryTest()
        {
            var documentCategory = new DocumentCategoryDto()
            {
                DocumentCategoryId = 10,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Invalid Model
            var result = controller.DeleteDocument(10000);
            Assert.IsType<NoContentResult>(result);

            //OK
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory);
            mockRepo.Setup(x => x.DeleteDocumentCategoryById(It.IsAny<int>())).Returns(true);
            controller.PostDocument(documentCategory);

            result = controller.DeleteDocument(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //File Type

        [Fact]
        public void GetFileTypeByIdTest()
        {
            var fileType = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostFile(fileType);

            var result = controller.GetFile(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllFilesTest()
        {
            var fileType = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var fileType2 = new FileTypeDto()
            {
                FileTypeId = 2,
                Extension = "PDF",
                IconPath = "link2.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostFile(fileType);
            controller.PostFile(fileType2);

            var fileTypes = controller.GetFiles();
            Assert.IsType<OkObjectResult>(fileTypes);
        }


        [Fact]
        public void AddFileTypeTest()
        {
            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);

            // Empty Model state
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.PostFile(new FileTypeDto());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            var fileType = new FileTypeDto()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateModified = DateTime.Now
            };
            result = controller.PostFile(fileType);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Validation

            fileType.DateCreated = DateTime.Now;
            fileType.FileTypeId = 1;
            result = controller.PostFile(fileType);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddFileTypeOk()
        {
            var fileType = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType);
            var result = controller.PostFile(fileType);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateFileTypeTest()
        {
            var fileType = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType);
            var controller = new AdminController(mockRepo.Object);
            controller.PostFile(fileType);

            //No content Result

            fileType.Extension = "doc";
            var result = controller.PutFile(1, fileType);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutFile(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteFileTypeTest()
        {
            var fileType = new FileTypeDto()
            {
                FileTypeId = 10,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Invalid Model
            var result = controller.DeleteFile(10000);
            Assert.IsType<NoContentResult>(result);

            //OK
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType);
            mockRepo.Setup(x => x.DeleteFileTypeById(It.IsAny<int>())).Returns(true);
            controller.PostFile(fileType);

            result = controller.DeleteFile(10);
            Assert.IsType<OkObjectResult>(result);

        }


    }
}