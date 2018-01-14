using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.PaymentGatewayRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class PaymentGatewayControllerTest
    {

        IkarusContext db = new IkarusContext();


        IPaymentGatewayRepository pgr => new PaymentGatewayRepository(db);
        IPaymentGatewayManipulation pgm => new PaymentGatewayManipulation(pgr);
        [Fact]
        public void GetPaymentGateways_ReturnsOK()
        {
            var controller = new PaymentGatewayController(pgm);
            // Act
            var result = controller.GetPaymentGateways();
            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetPaymentGateways_ReturnsNOK()
        {
            var mockSub = new Mock<IPaymentGatewayManipulation>();
            var controller = new PaymentGatewayController(mockSub.Object);
            // Act
            var result = controller.GetPaymentGateways();
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockSub = new Mock<IPaymentGatewayManipulation>();
            var controller = new PaymentGatewayController(mockSub.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(paymentGateway: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetPaymentGateway_ReturnsOK()
        {
            var controller = new PaymentGatewayController(pgm);
            // Act
            var result = controller.GetPaymentGateway(1);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPricingPackage_ReturnsNOK()
        {
            var controller = new PaymentGatewayController(pgm);
            // Act
            var result = controller.GetPaymentGateway(7);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
       
        [Fact]
        public void Post_ReturnsNOK()
        {
            var controller = new PaymentGatewayController(pgm);
            // Act
            PaymentGatewayDto pgdo = new PaymentGatewayDto();
            var result = controller.Post(pgdo);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

      


        
       
        
    }
}