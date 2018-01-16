using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.PricingPackageRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class PricinPackageControllerTest
    {

        IkarusContext db = new IkarusContext();

        IPricingPackageRepository ppr => new PricingPackageRepository(db);
        IPricingPackageManipulation ippm => new PricingPackageManipulation(ppr);


        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockSub = new Mock<IPricingPackageManipulation>();
            var controller = new PricingPackageController(mockSub.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(pricingPackage: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetPricingPackages_ReturnsOK()
        {
            var mockSub = new Mock<IPricingPackageManipulation>();
            var controller = new PricingPackageController(mockSub.Object);
            // Act
            var result = controller.GetPricingPackages();
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetPricingPackages_ReturnsNOK()
        {
            var controller = new PricingPackageController(ippm);
            // Act
            var result = controller.GetPricingPackages();
            // Assert
            Assert.NotEmpty(result);
        }


        [Fact]
        public void GetPricingPackage_ReturnsOK()
        {
            var controller = new PricingPackageController(ippm);
            // Act
            var result = controller.GetPricingPackage(2);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPricingPackage_ReturnsNOK()
        {
            var controller = new PricingPackageController(ippm);
            // Act
            var result = controller.GetPricingPackage(1);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetActivePackages_ReturnsNOK()
        {
            var mockSub = new Mock<IPricingPackageManipulation>();
            var controller = new PricingPackageController(mockSub.Object);
            // Act
            var result = controller.GetActivePackages();
            // Assert
            Assert.IsType<System.Collections.Generic.List<PricingPackageDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetActivePackages_ReturnsOK()
        {
            var controller = new PricingPackageController(ippm);
            // Act
            var result = controller.GetActivePackages();
            // Assert
            Assert.IsType<System.Collections.Generic.List<PricingPackageDto>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Post_ReturnsOK()
        {
            var controller = new PricingPackageController(ippm);
            // Act
            PricingPackageDto ppdo = new PricingPackageDto();
            var result = controller.Post(ppdo);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

      


        
       
        
    }
}