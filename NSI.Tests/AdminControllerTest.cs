using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AdminRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using NSI.REST.Models;
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
        public void AddCaseCategoryTest()
        {
            var mockRepo = new Mock<IAdminManipulation>();
            var controller = new AdminController(mockRepo.Object);
            // Empty Model state
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.PostCase(new CaseCategoryCreateModel());
            Assert.IsType<BadRequestObjectResult>(result);

            //Empty Model
            controller.ModelState.AddModelError("error", "some error");
            result = controller.PostCase(null);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            NSI.REST.Models.CaseCategoryCreateModel caseCategory = new CaseCategoryCreateModel()
            {
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
        public void AddCaseCategoryNoContentTest()
        {
            NSI.REST.Models.CaseCategoryCreateModel caseCategory = new CaseCategoryCreateModel()
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateCaseCategory(It.IsAny<CaseCategoryDto>()));
            var controller = new AdminController(mockRepo.Object);
            var result = controller.PostCase(caseCategory);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void AddCaseOk()
        {
            NSI.REST.Models.CaseCategoryCreateModel caseCategory = new CaseCategoryCreateModel()
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var caseCategory1 = new CaseCategoryDto
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.CreateCaseCategory(It.IsAny<CaseCategoryDto>())).Returns(caseCategory1);
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PostCase(caseCategory);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateCaseCategoryTest()
        {

            NSI.REST.Models.CaseCategoryCreateModel caseCategoryPost = new CaseCategoryCreateModel()
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            NSI.REST.Models.CaseCategoryEditModel caseCategoryPut = new CaseCategoryEditModel()
            {
                CaseCategoryId = 10,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

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
            controller.PostCase(caseCategoryPost);

            //No content Result

            caseCategory.CaseCategoryName = "NewName";
            var result = controller.PutCase(1, caseCategoryPut);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutCase(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateCaseCategoryNoContentTest()
        {
            NSI.REST.Models.CaseCategoryEditModel caseCategoryPut = new CaseCategoryEditModel()
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditCaseCategory(It.IsAny<int>(), It.IsAny<CaseCategoryDto>()));
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutCase(10, caseCategoryPut);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCaseCategoryOkTest()
        {

            NSI.REST.Models.CaseCategoryEditModel caseCategoryPut = new CaseCategoryEditModel()
            {
                CaseCategoryId = 10,
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditCaseCategory(It.IsAny<int>(),It.IsAny<CaseCategoryDto>())).Returns(true);
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutCase(10, caseCategoryPut);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteCaseCategoryTest()
        {
            NSI.REST.Models.CaseCategoryCreateModel caseCategory1 = new CaseCategoryCreateModel()
            {
                CaseCategoryName = "firstCase",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

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
            controller.PostCase(caseCategory1);

            result = controller.DeleteCase(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //Client Type

        [Fact]
        public void GetClientTypeByIdTest()
        {
            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var clientType1 = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostClient(clientType);

            var result = controller.GetClient(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllClientsTest()
        {
            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var clientType1 = new ClientTypeDto()
            {
                ClientTypeId = 1,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostClient(clientType);

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

            var result = controller.PostClient(new ClientTypeCreateModel());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
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
        public void AddClientTypeNoContentTest()
        {
            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>()));
            var controller = new AdminController(mockRepo.Object);
            var result = controller.PostClient(clientType);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void AddClientTypeOk()
        {
            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var clientType1 = new ClientTypeDto()
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
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType1);
            var result = controller.PostClient(clientType);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateClientTypeTest()
        {
            NSI.REST.Models.ClientTypeCreateModel clientTypePost = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            NSI.REST.Models.ClientTypeEditModel clientTypePut = new ClientTypeEditModel()
            {
                ClientTypeId = 10,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

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
            controller.PostClient(clientTypePost);

            //No content Result

            clientTypePut.ClientTypeName = "NewName";
            var result = controller.PutClient(1, clientTypePut);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutClient(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateClientTypeNoContentTest()
        {
            NSI.REST.Models.ClientTypeEditModel clientTypePut = new ClientTypeEditModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditCaseCategory(It.IsAny<int>(), It.IsAny<CaseCategoryDto>()));
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutClient(10, clientTypePut);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateClientTypeOkTest()
        {
            NSI.REST.Models.ClientTypeEditModel clientTypePut = new ClientTypeEditModel()
            {
                ClientTypeId = 10,
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };


            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditClientType(It.IsAny<int>(), It.IsAny<ClientTypeDto>())).Returns(true);
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutClient(10, clientTypePut);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteClientTypeTest()
        {
            NSI.REST.Models.ClientTypeCreateModel clientType = new ClientTypeCreateModel()
            {
                ClientTypeName = "firstClient",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var clientType1 = new ClientTypeDto()
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
            mockRepo.Setup(x => x.CreateClientType(It.IsAny<ClientTypeDto>())).Returns(clientType1);
            mockRepo.Setup(x => x.DeleteClientTypeById(It.IsAny<int>())).Returns(true);
            controller.PostClient(clientType);

            result = controller.DeleteClient(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //Document Category

        [Fact]
        public void GetDocumentCategoryByIdTest()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var documentCategory1 = new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostDocument(documentCategory);

            var result = controller.GetDocument(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllDocumentsTest()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var documentCategory1= new DocumentCategoryDto()
            {
                DocumentCategoryId = 1,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };


            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostDocument(documentCategory);

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

            var result = controller.PostDocument(new DocumentCategoryCreateModel());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 

            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
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
        public void AddDocumentCategoryNoContentTest()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>()));
            var controller = new AdminController(mockRepo.Object);
            var result = controller.PostDocument(documentCategory);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void AddDocumentCategoryOk()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var documentCategory1 = new DocumentCategoryDto()
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
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory1);
            var result = controller.PostDocument(documentCategory);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateDocumentCategoryTest()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategoryPost = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            NSI.REST.Models.DocumentCategoryEditModel documentCategoryPut = new DocumentCategoryEditModel()
            {
                DocumentCategoryId = 10,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

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
            controller.PostDocument(documentCategoryPost);

            //No content Result

            documentCategoryPut.DocumentCategoryTitle = "NewName";
            var result = controller.PutDocument(1, documentCategoryPut);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutDocument(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateDocumentCategoryNoContentTest()
        {
            NSI.REST.Models.DocumentCategoryEditModel documentCategoryPut = new DocumentCategoryEditModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditCaseCategory(It.IsAny<int>(), It.IsAny<CaseCategoryDto>()));
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutDocument(10, documentCategoryPut);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateDocumentCategoryOkTest()
        {
            NSI.REST.Models.DocumentCategoryEditModel documentCategoryPut = new DocumentCategoryEditModel()
            {
                DocumentCategoryId = 10,
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditDocumentCategory(It.IsAny<int>(), It.IsAny<DocumentCategoryDto>())).Returns(true);
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutDocument(10, documentCategoryPut);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteDocumentCategoryTest()
        {
            NSI.REST.Models.DocumentCategoryCreateModel documentCategory = new DocumentCategoryCreateModel()
            {
                DocumentCategoryTitle = "firstDocument",
                IsDeleted = false,
                CustomerId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var documentCategory1 = new DocumentCategoryDto()
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
            mockRepo.Setup(x => x.CreateDocumentCategory(It.IsAny<DocumentCategoryDto>())).Returns(documentCategory1);
            mockRepo.Setup(x => x.DeleteDocumentCategoryById(It.IsAny<int>())).Returns(true);
            controller.PostDocument(documentCategory);

            result = controller.DeleteDocument(10);
            Assert.IsType<OkObjectResult>(result);

        }

        //File Type

        [Fact]
        public void GetFileTypeByIdTest()
        {
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var fileType1 = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostFile(fileType);

            var result = controller.GetFile(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllFilesTest()
        {
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var fileType1 = new FileTypeDto()
            {
                FileTypeId = 1,
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType1);
            var controller = new AdminController(mockRepo.Object);
            controller.PostFile(fileType);

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

            var result = controller.PostFile(new FileTypeCreateModel());
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Case Model 
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
            };

            var fileType1 = new FileTypeDto()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
            };
            result = controller.PostFile(fileType);
            Assert.IsType<BadRequestObjectResult>(result);

            //Bad Validation

            fileType.DateCreated = DateTime.Now;
            result = controller.PostFile(fileType);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddFileTypeNoContentTest()
        {
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminManipulation>();
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>()));
            var controller = new AdminController(mockRepo.Object);
            var result = controller.PostFile(fileType);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void AddFileTypeOk()
        {
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var fileType1 = new FileTypeDto()
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
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType1);
            var result = controller.PostFile(fileType);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void UpdateFileTypeTest()
        {
            NSI.REST.Models.FileTypeCreateModel fileTypePost = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            NSI.REST.Models.FileTypeEditModel fileTypePut = new FileTypeEditModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

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
            controller.PostFile(fileTypePost);

            //No content Result

            fileType.Extension = "doc";
            var result = controller.PutFile(1, fileTypePut);
            Assert.IsType<NoContentResult>(result);

            //Bad Case Model
            controller.ModelState.AddModelError("error", "some error");

            result = controller.PutFile(30, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateFileTypeNoContentTest()
        {
            NSI.REST.Models.FileTypeEditModel fileTypePut = new FileTypeEditModel()
            {
                Extension = "firstClient",
                IsDeleted = false,
                IconPath = "link.com",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditCaseCategory(It.IsAny<int>(), It.IsAny<CaseCategoryDto>()));
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutFile(10, fileTypePut);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateFileTypeOkTest()
        {
            NSI.REST.Models.FileTypeEditModel fileTypePut = new FileTypeEditModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(x => x.EditFileType(It.IsAny<int>(), It.IsAny<FileTypeDto>())).Returns(true);
            var adminManipulation = new AdminManipulation(mockRepo.Object);
            var controller = new AdminController(adminManipulation);
            var result = controller.PutFile(1, fileTypePut);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteFileTypeTest()
        {
            NSI.REST.Models.FileTypeCreateModel fileType = new FileTypeCreateModel()
            {
                Extension = "PDF",
                IconPath = "link.com",
                IsDeleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var fileType1 = new FileTypeDto()
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
            mockRepo.Setup(x => x.CreateFileType(It.IsAny<FileTypeDto>())).Returns(fileType1);
            mockRepo.Setup(x => x.DeleteFileTypeById(It.IsAny<int>())).Returns(true);
            controller.PostFile(fileType);

            result = controller.DeleteFile(10);
            Assert.IsType<OkObjectResult>(result);

        }

    }
}