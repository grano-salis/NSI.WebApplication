using System;
using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.ClientsRepository;
using NSI.DC.Exceptions;
using NSI.Repository.Interfaces;
using NSI.Repository.Repository;
using NSI.REST.Controllers;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace NSI.Tests
{
    public class ClientsControllerTest
    {
        IkarusContext db = new IkarusContext();

        IClientRepository cR => new ClientRepository(db);
        IClientManipulation cM => new ClientManipulation(cR);

        [Fact]
        public void GetClientById_ReturnsOKModel()
        {
            var controller = new ClientsController(cM);

            var result = controller.GetClientById(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetClientById_ReturnsNOKModel()
        {

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.GetClientById(It.IsAny<int>())).Returns((ClientDto)null);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);
            var result = controller.GetClientById(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddClientOk()
        {
            var client = new ClientDto()
            {
                ClientId = 1,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);

            var result = controller.CreateNewClient(client);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void AddClientBadModel()
        {
            var client = new ClientDto()
            {
                ClientId = 1
            };
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Throws<NSIException>();
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);

            var result = controller.CreateNewClient(client);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void AddClientEmptyModel()
        {
            var client = new ClientDto() { };
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);

            var result = controller.CreateNewClient(client);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void AddClientEmptyModelState()
        {
            var client = new ClientDto()
            {
                ClientId = 1,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.CreateNewClient(new ClientDto());
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void UpdateClient_ReturnsBadObjectResult()
        {
            var client = new ClientDto()
            {
                ClientId = 1,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.EditClient(It.IsAny<ClientDto>())).Returns(false);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);

            var result = controller.EditClient(client);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateClient_ReturnsOkResult()
        {
            var client = new ClientDto()
            {
                ClientId = 1115,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            mockRepo.Setup(x => x.EditClient(It.IsAny<ClientDto>())).Returns(true);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);
            controller.CreateNewClient(client);

            client.ClientName = "Sakib Kurtic";

            var result = controller.EditClient(client);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateClient_ReturnsNoContentResult()
        {
            var client = new ClientDto()
            {
                ClientId = 1115,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };

            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            mockRepo.Setup(x => x.EditClient(It.IsAny<ClientDto>())).Returns(false);
            var clientManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(clientManipulation);
            controller.CreateNewClient(client);

            client.ClientName = "Sakib Kurtic";

            var result = controller.EditClient(client);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            var mockRepo = new Mock<IClientManipulation>();
            var controller = new ClientsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.EditClient(clientDto : null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IClientManipulation>();
            var controller = new ClientsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.DeleteClient(500);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void Delete_ReturnsOK()
        {
            int clientId = 1115;
            var client = new ClientDto()
            {
                ClientId = 1115,
                ClientName = "Sakib",
                ClientTypeId = 1,
                CustomerId = 1,
                AddressId = 1,
                CreatedByUserId = 6,
                DateCreated = System.DateTime.Now,
                IsDeleted = false,
                DateModified = null
            };
            var mockRepo = new Mock<NSI.Repository.Interfaces.IClientRepository>();
            mockRepo.Setup(x => x.CreateClient(It.IsAny<ClientDto>())).Returns(client);
            mockRepo.Setup(x => x.DeleteClientById(1115)).Returns(true);
            var cliManipulation = new ClientManipulation(mockRepo.Object);
            var controller = new ClientsController(cliManipulation);
            controller.CreateNewClient(client);
            var result = controller.DeleteClient(clientId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


    }
}
