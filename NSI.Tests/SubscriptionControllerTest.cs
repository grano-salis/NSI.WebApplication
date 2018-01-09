using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.SubscriptionRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class SubscriptionControllerTest
    {

        IkarusContext db = new IkarusContext();
       
        ISubscriptionRepository isr => new SubscriptionRepository(db);
        IPricingPackageRepository ipr => new PricingPackageRepository(db);
        ISubscriptionManipulation ism => new SubscriptionManipulation(isr, ipr);



        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockSub = new Mock<ISubscriptionManipulation>();
            var controller = new SubscriptionController(mockSub.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(subscription: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetAllSubscriptions_ReturnsOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetAllSubscriptions();
            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAllSubscriptions_ReturnsNOK()
        {
            var mockSub = new Mock<ISubscriptionManipulation>();
            var controller = new SubscriptionController(mockSub.Object);
            // Act
            var result = controller.GetAllSubscriptions();
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetSubscription_ReturnsOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetSubscription(2);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSubscription_ReturnsNOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetSubscription(1);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetActiveSubscription_ReturnsOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetActiveSubscription(1);
            // Assert
            Assert.IsType<SubscriptionDto>(result);
        }

        [Fact]
        public void GetActiveSubscription_ReturnsNOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetActiveSubscription(2);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Post_ReturnsNOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            SubscriptionDto sub = new SubscriptionDto();
            var result = controller.Post(sub);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_ReturnsNOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            SubscriptionDto sub = new SubscriptionDto();
            var result = controller.Put(sub);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetBonusDays_ReturnsOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetBonusDays(2,2);
            // Assert
            Assert.IsType<Int32>(result);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void GetBonusDays_ReturnsNOK()
        {
            var controller = new SubscriptionController(ism);
            // Act
            var result = controller.GetBonusDays(245, 2);
            // Assert
            Assert.IsType<Int32>(result);
            Assert.Equal(0, result);
        }



    }
}