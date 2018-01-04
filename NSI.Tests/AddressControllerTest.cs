using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AddressRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace NSI.Tests
{
    public class AddressControllerTest
    {
        private readonly ITestOutputHelper output;

        public AddressControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void getAddressByIdTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var mockRepo = new Mock<IAddressRepository>();
            mockRepo.Setup(x => x.GetAddressById(It.Is<int>(q => q == 1))).Returns(address);
            var addressManipulation = new AddressManipulation(mockRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.GetAddress(1);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as AddressDto;
            Assert.NotNull(model);
            output.WriteLine(model.Address1);
            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public void getAddressesTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            var address = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var addresses = new List<AddressDto>
            {
                address
            };

            var mockRepo = new Mock<IAddressRepository>();
            mockRepo.Setup(x => x.GetAddreses()).Returns(addresses);
            var addressManipulation = new AddressManipulation(mockRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.GetAddreses();
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void CreateAddressFailureTest()
        {

            var mockRepo = new Mock<IAddressManipulation>();
            var controller = new AddressController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.PostAddress(null);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void CreateNewAddressSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            NSI.REST.Models.AddressCreateModel address = new AddressCreateModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                DateCreated = DateCreated,
                DateModified = DateModified

            };

            var address2 = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            var mockRepo = new Mock<IAddressRepository>();
            mockRepo.Setup(x => x.CreateAddress(It.IsAny<AddressDto>())).Returns(address2);
            var addressManipulation = new AddressManipulation(mockRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.PostAddress(address);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateNewAddressBadRequestFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            NSI.REST.Models.AddressCreateModel address = new AddressCreateModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                DateCreated = DateCreated,
                DateModified = DateModified

            };

            var mockRepo = new Mock<IAddressManipulation>();
            mockRepo.Setup(x => x.CreateAddress(It.IsAny<AddressDto>())).Throws<Exception>();
            var controller = new AddressController(mockRepo.Object);
            var result = controller.PostAddress(address);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateNewAddressNoContentFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "71000";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;
            DateTime DateCreated = DateTime.Now;
            DateTime DateModified = DateTime.Now;

            NSI.REST.Models.AddressCreateModel address = new AddressCreateModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                DateCreated = DateCreated,
                DateModified = DateModified

            };

            var mockRepo = new Mock<IAddressManipulation>();
            mockRepo.Setup(x => x.CreateAddress(It.IsAny<AddressDto>()));
            var controller = new AddressController(mockRepo.Object);
            var result = controller.PostAddress(address);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateAddressFailureTest()
        {
            var mockRepo = new Mock<IAddressManipulation>();
            var controller = new AddressController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.PutAddress(1,model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateAddressSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "Test";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressEditModel address = new AddressEditModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                IsDeleted = isDeleted,
                AddressTypeId = AddressTypeId,
                DateModified = DateTime.Now

            };

            var address2 = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var addressRepo = new Mock<IAddressRepository>();
            addressRepo.Setup(x => x.EditAddress(It.IsAny<int>(),It.IsAny<AddressDto>())).Returns(true);
            var addressManipulation = new AddressManipulation(addressRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.PutAddress(1, address);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void UpdateAddressBadRequestFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "Test";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressEditModel address = new AddressEditModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                IsDeleted = isDeleted,
                AddressTypeId = AddressTypeId,
                DateModified = DateTime.Now

            };

            var addressRepo = new Mock<IAddressRepository>();
            addressRepo.Setup(x => x.EditAddress(It.IsAny<int>(), It.IsAny<AddressDto>())).Throws<Exception>();
            var addressManipulation = new AddressManipulation(addressRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.PutAddress(1, address);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateAddressNoContentFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "Test";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressEditModel address = new AddressEditModel()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                IsDeleted = isDeleted,
                AddressTypeId = AddressTypeId,
                DateModified = DateTime.Now

            };

            var addressRepo = new Mock<IAddressRepository>();
            addressRepo.Setup(x => x.EditAddress(It.IsAny<int>(), It.IsAny<AddressDto>()));
            var addressManipulation = new AddressManipulation(addressRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.PutAddress(1, address);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteNonExistentAddressFailureTest()
        {
            // Arrange & Act
            var mockRepo = new Mock<IAddressManipulation>();
            var controller = new AddressController(mockRepo.Object);

            // Act
            var result = controller.DeleteAddress(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void DeleteAddressSuccessTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "Test";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressCreateModel address = new AddressCreateModel()
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now

            };

            var address2 = new AddressDto
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            var addressRepo = new Mock<IAddressRepository>();
            addressRepo.Setup(x => x.DeleteAddressById(It.IsAny<int>())).Returns(true);
            var addressManipulation = new AddressManipulation(addressRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.DeleteAddress(1);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteAddressBadRequestFailureTest()
        {
            int id = 1;
            string Address1 = "Test";
            string Address2 = "Test";
            string City = "Test";
            string ZipCode = "Test";
            bool isDeleted = false;
            int AddressTypeId = 1;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressCreateModel address = new AddressCreateModel()
            {
                AddressId = id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                ZipCode = ZipCode,
                AddressTypeId = AddressTypeId,
                CreatedByUserId = CreatedByUserId,
                IsDeleted = isDeleted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now

            };

            var addressRepo = new Mock<IAddressRepository>();
            addressRepo.Setup(x => x.DeleteAddressById(It.IsAny<int>())).Throws<Exception>();
            var addressManipulation = new AddressManipulation(addressRepo.Object);
            var controller = new AddressController(addressManipulation);
            var result = controller.DeleteAddress(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
