using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AddressTypeRepository;
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
    public class AddressTypeControllerTest
    {

        IkarusContext db = new IkarusContext();
        IAddressTypeRepository iar => new AddressTypeRepository(db);
        IAddressTypeManipulation iam => new AddressTypeManipulation(iar);

        private readonly ITestOutputHelper output;

        public AddressTypeControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAddressTypeByIdTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            var addressType_1 = new AddressTypeDto
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var mockRepo = new Mock<IAddressTypeRepository>();
            mockRepo.Setup(x => x.GetAddressTypeById(It.Is<int>(q => q == 1))).Returns(addressType_1);
            var addressTypeManipulation = new AddressTypeManipulation(mockRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.GetAddressType(1);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as AddressTypeDto;
            Assert.NotNull(model);
            output.WriteLine(model.AddressTypeName);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAddressTypeTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            var addressType_1 = new AddressTypeDto
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var addresses = new List<AddressTypeDto>
            {
                addressType_1
            };

            var mockRepo = new Mock<IAddressTypeRepository>();
            mockRepo.Setup(x => x.GetAddressTypes()).Returns(addresses);
            var addressTypeManipulation = new AddressTypeManipulation(mockRepo.Object);

            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.GetAddressTypes();
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateAddressTypeFailureTest()
        {
            var mockRepo = new Mock<IAddressTypeManipulation>();
            var controller = new AddressTypeController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "AddressType error");
            var result = controller.PostAddressType(model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateNewAddressTypeSuccessTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType_1 = new AddressTypeCreateModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressType_2 = new AddressTypeDto
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var mockRepo = new Mock<IAddressTypeRepository>();
            mockRepo.Setup(x => x.CreateAddressType(It.IsAny<AddressTypeDto>())).Returns(addressType_2);
            var addressTypeManipulation = new AddressTypeManipulation(mockRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.PostAddressType(addressType_1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateNewAddressTypeBadRequestFailureTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType_1 = new AddressTypeCreateModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var mockRepo = new Mock<IAddressTypeManipulation>();
            mockRepo.Setup(x => x.CreateAddressType(It.IsAny<AddressTypeDto>())).Throws<Exception>();
            var controller = new AddressTypeController(mockRepo.Object);
            var result = controller.PostAddressType(addressType_1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateNewAddressTypeNoContentFailureTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType_1 = new AddressTypeCreateModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var mockRepo = new Mock<IAddressTypeManipulation>();
            mockRepo.Setup(x => x.CreateAddressType(It.IsAny<AddressTypeDto>()));
            var controller = new AddressTypeController(mockRepo.Object);
            var result = controller.PostAddressType(addressType_1);

            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public void UpdateAddressTypeFailureTest()
        {
            var mockRepo = new Mock<IAddressTypeManipulation>();
            var controller = new AddressTypeController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "AddressType error");
            var result = controller.PutAddressType(1, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateAddressTypeSuccessTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeEditModel addressType_1 = new AddressTypeEditModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressTypeRepo = new Mock<IAddressTypeRepository>();
            addressTypeRepo.Setup(x => x.EditAddressType(It.IsAny<int>(), It.IsAny<AddressTypeDto>())).Returns(true);
            var addressTypeManipulation = new AddressTypeManipulation(addressTypeRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.PutAddressType(1, addressType_1);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void UpdateAddressTypeBadRequestFailureTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeEditModel addressType_1 = new AddressTypeEditModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressRepo = new Mock<IAddressTypeRepository>();
            addressRepo.Setup(x => x.EditAddressType(It.IsAny<int>(), It.IsAny<AddressTypeDto>())).Throws<Exception>();
            var addressTypeManipulation = new AddressTypeManipulation(addressRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.PutAddressType(1, addressType_1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateAddressTypeNoContentFailureTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeEditModel addressType_1 = new AddressTypeEditModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressRepo = new Mock<IAddressTypeRepository>();
            addressRepo.Setup(x => x.EditAddressType(It.IsAny<int>(), It.IsAny<AddressTypeDto>()));
            var addressTypeManipulation = new AddressTypeManipulation(addressRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.PutAddressType(1, addressType_1);
            Assert.IsType<NoContentResult>(result);
        }

        
        [Fact]
        public void DeleteNonExistentAddressTypeFailureTest()
        {
            var mockRepo = new Mock<IAddressTypeManipulation>();
            var controller = new AddressTypeController(mockRepo.Object);
            var result = controller.DeleteAddressType(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAddressTypeSuccessTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType_1 = new AddressTypeCreateModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressRepo = new Mock<IAddressTypeRepository>();
            addressRepo.Setup(x => x.DeleteAddressTypeById(It.IsAny<int>())).Returns(true);
            var addressTypeManipulation = new AddressTypeManipulation(addressRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.DeleteAddressType(1);
            Assert.IsType<NoContentResult>(result);
            
        }

        [Fact]
        public void DeleteAddressBadRequestFailureTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType_1 = new AddressTypeCreateModel()
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var addressRepo = new Mock<IAddressTypeRepository>();
            addressRepo.Setup(x => x.DeleteAddressTypeById(It.IsAny<int>())).Throws<Exception>();
            var addressTypeManipulation = new AddressTypeManipulation(addressRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.DeleteAddressType(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
