using AutoMapper;
using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.AddressRepository;
using NSI.DC.CaseRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.AutoMapper;
using NSI.REST.Controllers;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace NSI.Tests
{
    public class CaseControllerTest
    {

        private readonly ITestOutputHelper output;

        public CaseControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void getCaseByIdTest()
        {
            var caseInfo = new CaseInfo
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var mockRepo = new Mock<ICaseInfoRepository>();
            mockRepo.Setup(x => x.GetCaseInfoById(It.Is<int>(q => q == 1))).Returns(caseInfo);
            var caseInfoManipulation = new CaseInfoManipulation(mockRepo.Object);
            IMapper _mapper = new Mapper(null);

            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.Get(1);
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as CaseInfo;
            Assert.NotNull(model);
            output.WriteLine(model.CaseNumber);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void geCaseInfosTest()
        {
            var caseInfo = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var addresses = new List<CaseInfoDto>
            {
                caseInfo
            };

            var mockRepo = new Mock<ICaseInfoRepository>();
            mockRepo.Setup(x => x.GetCaseInfos()).Returns(addresses);
            var caseInfoManipulation = new CaseInfoManipulation(mockRepo.Object);
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.Get();
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void CreateCaseInfoFailureTest()
        {

            var mockRepo = new Mock<ICaseInfoManipulation>();
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(mockRepo.Object, _mapper);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.Post(null);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void CreateNewCaseInfoSuccessTest()
        {
            var caseInfo = new CaseInfo
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var mockRepo = new Mock<ICaseInfoRepository>();
            mockRepo.Setup(x => x.CreateCaseInfo(It.IsAny<CaseInfoDto>())).Returns(caseInfo);
            var caseInfoManipulation = new CaseInfoManipulation(mockRepo.Object);
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.Post(caseInfoDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateNewCaseInfoBadRequestFailureTest()
        {
            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var mockRepo = new Mock<ICaseInfoManipulation>();
            mockRepo.Setup(x => x.CreateCaseInfo(It.IsAny<CaseInfoDto>())).Throws<Exception>();
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(mockRepo.Object, _mapper);
            var result = controller.Post(caseInfoDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateCaseInfoNoContentFailureTest()
        {
            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var mockRepo = new Mock<ICaseInfoManipulation>();
            mockRepo.Setup(x => x.CreateCaseInfo(It.IsAny<CaseInfoDto>()));
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(mockRepo.Object, _mapper);
            var result = controller.Post(caseInfoDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCaseInfoFailureTest()
        {
            var mockRepo = new Mock<ICaseInfoManipulation>();
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(mockRepo.Object, _mapper);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.Put(1, null);
            Assert.IsType<Boolean>(result);
        }

        [Fact]
        public void UpdateCaseInfoSuccessTest()
        {
            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var caseInfoRepo = new Mock<ICaseInfoRepository>();
            caseInfoRepo.Setup(x => x.EditCaseInfoById(It.IsAny<int>(), It.IsAny<CaseInfoDto>())).Returns(true);
            var caseInfoManipulation = new CaseInfoManipulation(caseInfoRepo.Object);
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.Put(1, caseInfoDto);
            Assert.IsType<Boolean>(result);
        }

        [Fact]
        public void UpdateCaseInfoBadRequestFailureTest()
        {
            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var caseInfoRepo = new Mock<ICaseInfoRepository>();
            caseInfoRepo.Setup(x => x.EditCaseInfoById(It.IsAny<int>(), It.IsAny<CaseInfoDto>())).Throws<Exception>();
            var caseInfoManipulation = new CaseInfoManipulation(caseInfoRepo.Object);
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.Put(1, caseInfoDto);
            Assert.IsType<Boolean>(result);
        } 

        [Fact]
        public void DeleteNonExistentCaseInfoFailureTest()
        {
            // Arrange & Act
            var mockRepo = new Mock<ICaseInfoManipulation>();
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(mockRepo.Object, _mapper);

            // Act
            var result = controller.DeleteCase(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void DeleteCaseInfoSuccessTest()
        {
            var caseInfoDto = new CaseInfoDto
            {
                CaseId = 1,
                CaseNumber = "1",
                CourtNumber = "1",
                Value = 1,
                Judge = 'F',
                Court = "Test",
                CounterParty = "Test",
                Note = "Test",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsDeleted = false,
                CaseCategory = 1,
                CustomerId = 1,
                ClientId = 1,
                CreatedByUserId = 1
            };

            var caseInfoRepo = new Mock<ICaseInfoRepository>();
            caseInfoRepo.Setup(x => x.DeleteCase(It.IsAny<int>()));
            var caseInfoManipulation = new CaseInfoManipulation(caseInfoRepo.Object);
            IMapper _mapper = new Mapper(null);
            var controller = new CaseInfoController(caseInfoManipulation, _mapper);
            var result = controller.DeleteCase(1);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
