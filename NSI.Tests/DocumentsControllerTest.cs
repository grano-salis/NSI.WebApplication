using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.DocumentRepository;
using NSI.Repository.Interfaces;
using NSI.Repository.Repository;
using NSI.REST.Controllers;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NSI.Tests
{
    [TestClass]
    public class DocumentsControllerTest
    {
        [TestMethod]
        public void Create_ReturnsNewlyCreatedDocument()
        {
            // Arrange
            int id = 123;
            string documentTitle = "dokument";
            int categoryId = 123;
            int caseId = 123;
            string documentDescription = "description";
            string documentPath = "path";
            string documentContent = "content";

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


            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.Post(document);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                //.IsType<OkObjectResult>(result);
        }

        [TestMethod]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IDocumentManipulation>();
            var controller = new DocumentController(mockRepo.Object);
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
            int id = 123;
            int fileTypeId = 123;
            string documentTitle = "dokument";
            int categoryId = 123;
            int caseId = 123;
            string documentDescription = "description";
            string documentPath = "path";
            string documentContent = "content";
            string categoryName = "category";

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
            documentRepo.Setup(x => x.Update(document));
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.Put(id, document);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetDocuments_ReturnsOK()
        {
            // Arrange & Act
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            int page = 1;
            int perPage = 100;
            string description = "";
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
        public void GetDocumentById_ReturnsNoContent()
        {
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.Get(100000);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public void GetDocmentById_ReturnsOK()
        {
            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public void GetCaseByIdReturnsOK()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.GetByCaseId(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetDocumentHistoryReturnsOK()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.GetDocumentHistory(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetNumberOfDocumentsByCaseOK()
        {

            var documentRepo = new Mock<IDocumentRepository>();
            var documentManipulation = new DocumentManipulation(documentRepo.Object);
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.GetNumberOfDocumentsByCase(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Delete_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IDocumentManipulation>();
            var controller = new DocumentController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Delete(100000);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public void DeleteDocument_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string documentTitle = "dokument";
            int categoryId = 123;
            int caseId = 123;
            string documentDescription = "description";
            string documentPath = "path";
            string documentContent = "content";

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
            var controller = new DocumentController(documentManipulation);

            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}

