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
            var controller = new AddressTypeController(iam);
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
            var controller = new AddressTypeController(iam);
            var result = controller.GetAddressTypes();
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
        public void UpdateAddressTypeFailureTest()
        {
            var mockRepo = new Mock<IAddressTypeManipulation>();
            var controller = new AddressTypeController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "AddressType error");
            var result = controller.PutAddressType(1, model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteNonExistentAddressTypeFailureTest()
        {
            var mockRepo = new Mock<IAddressTypeManipulation>();
            var controller = new AddressTypeController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "AddressType error");
            var result = controller.DeleteAddressType(200);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAddressTypeSuccessTest()
        {
            int id = 1;
            string TypeName = "Test";
            bool isDeleted = false;
            int CreatedByUserId = 1;

            NSI.REST.Models.AddressTypeCreateModel addressType = new AddressTypeCreateModel()
            {
                AddressTypeName=TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId=CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };

            var adesdrsType_1 = new AddressTypeDto
            {
                AddressTypeName = TypeName,
                AddressTypeId = id,
                IsDeleted = isDeleted,
                CustomerId = CreatedByUserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var addressTypeRepo = new Mock<IAddressTypeRepository>();
            addressTypeRepo.Setup(x => x.CreateAddressType(adesdrsType_1));
            var addressTypeManipulation = new AddressTypeManipulation(addressTypeRepo.Object);
            var controller = new AddressTypeController(addressTypeManipulation);
            var result = controller.DeleteAddressType(1);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
