using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.DocumentRepository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.DC.Exceptions;

namespace NSI.Tests
{
    [TestClass]
    public class DocumentsControllerTest
    {
        [TestMethod]
        public void Create_ReturnsNewlyCreatedDocument()
        {
            // Arrange
            const int id = 123;
            const string documentTitle = "dokument";
            const int categoryId = 123;
            const int caseId = 123;
            const string documentDescription = "description";
            const string documentPath = "path";
            const string documentContent = "content";

            var document = new CreateDocumentDto()
            {
                DocumentId = id,
                CaseId = caseId,
                DocumentTitle = documentTitle,
                CategoryId = categoryId,
                DocumentDescription = documentDescription,
                DocumentPath = documentPath,
                DocumentContent = documentContent
            };

            var documentRepo = new Mock<IDocumentRepository>();
            documentRepo.Setup(x => x.SaveDocument(document));
            var documentManipulation = new DocumentManipulation(documentRepo.Object);

            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.Post(document);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                //.IsType<OkObjectResult>(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void Create_BadCategoryId_ThrowsException()
        {
            // Arrange
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.Post(null);
        }

        [TestMethod]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IDocumentManipulation>();
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(mockRepo.Object, logger);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Put(100000000, null);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UpdateDocument_ReturnsOK()
        {
            // Arrange
            const int id = 123;
            const int fileTypeId = 123;
            const string documentTitle = "dokument";
            const int categoryId = 123;
            const int caseId = 123;
            const string documentDescription = "description";
            const string documentPath = "path";
            const string documentContent = "content";
            var categoryName = "category";

            var document = new DocumentDto()
            {
                DocumentId = id,
                CaseId = caseId,
                FileTypeId = fileTypeId,
                DocumentTitle = documentTitle,
                CategoryId = categoryId,
                CategoryName = categoryName,
                DocumentDescription = documentDescription,
                DocumentPath = documentPath,
                DocumentContent = documentContent,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now
            };

            var documentRepo = new Mock<IDocumentRepository>();

            documentRepo.Setup(x => x.Update(It.Is<DocumentDto>(dto => id==123))).Returns(1);
            documentRepo.Setup(x => x.GetDocument(It.IsAny<int>())).Returns(new DocumentDetails(){DocumentId = 123});
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.Put(id, document);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }


        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void UpdateDocument_InvalidId_ThrowException()
        {
            // Arrange
            const int id = -1;
            const int fileTypeId = 123;
            const string documentTitle = "dokument";
            const int categoryId = 123;
            const int caseId = 123;
            const string documentDescription = "description";
            const string documentPath = "path";
            const string documentContent = "content";
            var categoryName = "category";

            var document = new DocumentDto()
            {
                DocumentId = id,
                CaseId = caseId,
                FileTypeId = fileTypeId,
                DocumentTitle = documentTitle,
                CategoryId = categoryId,
                CategoryName = categoryName,
                DocumentDescription = documentDescription,
                DocumentPath = documentPath,
                DocumentContent = documentContent,
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now
            };

            var documentRepo = new Mock<IDocumentRepository>();

            documentRepo.Setup(x => x.Update(It.Is<DocumentDto>(dto => id == 123))).Returns(1);
            documentRepo.Setup(x => x.GetDocument(It.IsAny<int>())).Returns(new DocumentDetails() { DocumentId = 123 });
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.Put(id, document);
        }

        [TestMethod]
        public void GetDocuments_ReturnsOK()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var page = 1;
            var perPage = 100;
            var description = "";
            var pageM = new DocumentsPagingQueryModel()
            {
                PageNumber = page,
                ResultsPerPage = perPage,
                SearchByDescription = description
            };
            var result = controller.GetDocumentsByPage(pageM);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetDocumentCategories_ReturnsOK()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.GetDocumentCategories();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Get_ReturnsOK()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void GetDocuments_InvalidId_ThrowException()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var page = -1;
            var perPage = 100;
            var description = "";
            var pageM = new DocumentsPagingQueryModel()
            {
                PageNumber = page,
                ResultsPerPage = perPage,
                SearchByDescription = description
            };
            controller.GetDocumentsByPage(pageM);

        }

        [TestMethod]
        public void GetDocumentById_ReturnsNoContent()
        {
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.Get(100000);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void GetDocumentById_InvalidId_ThrowException()
        {
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.Get(-1);
        }

        [TestMethod]
        public void GetDocmentById_ReturnsOK()
        {
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public void GetCaseByIdReturnsOk()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.GetByCaseId(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void GetCaseById_InvalidId_ThrowException()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.GetByCaseId(-1);
        }

        [TestMethod]
        public void GetDocumentHistoryReturnsOk()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.GetDocumentHistory(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void GetDocumentHistory_InvalidId_ThrowException()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.GetDocumentHistory(-1);
        }

        [TestMethod]
        public void GetNumberOfDocumentsByCaseOk()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            var result = controller.GetNumberOfDocumentsByCase(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void GetNumberOfDocumentsByCase_InvalidId_ThrowException()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.GetNumberOfDocumentsByCase(-1);
        }

        [TestMethod]
        public void Delete_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IDocumentManipulation>();
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(mockRepo.Object, logger);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Delete(100000);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        [ExpectedException(typeof(NSIException))]
        public void Delete_InvalidId_ThrowException()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);

            // Act
            controller.Delete(0);
        }


        [TestMethod]
        public void DeleteDocument_ReturnsOK()
        {
            // Arrange
            var id = 123;
            var documentTitle = "dokument";
            var categoryId = 123;
            var caseId = 123;
            var documentDescription = "description";
            var documentPath = "path";
            var documentContent = "content";

            var document = new CreateDocumentDto()
            {
                DocumentId = id,
                CaseId = caseId,
                DocumentTitle = documentTitle,
                CategoryId = categoryId,
                DocumentDescription = documentDescription,
                DocumentPath = documentPath,
                DocumentContent = documentContent
            };

            var documentRepo = new Mock<IDocumentRepository>();
            documentRepo.Setup(x => x.SaveDocument(document));
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var logger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentController>>().Object;
            var controller = new DocumentController(documentManipulation, logger);
            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }

    

}

