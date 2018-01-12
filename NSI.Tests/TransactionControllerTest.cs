using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.TransactionRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class TransactionControllerTest
    {
        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<ITransactionManipulation>();
            var mockSub = new Mock<ISubscriptionManipulation>();
            var controller = new TransactionController(mockRepo.Object, mockSub.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(transaction: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        IkarusContext db = new IkarusContext();
        ITransactionRepository itr => new TransactionRepository(db);
        ITransactionManipulation itm => new TransactionManipulation(itr);
        ISubscriptionRepository isr => new SubscriptionRepository(db);
        IPricingPackageRepository ipr => new PricingPackageRepository(db);
        ISubscriptionManipulation ism => new SubscriptionManipulation(isr, ipr);


        [Fact]
        public void GetTransaction_ReturnsOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.GetTransaction(113);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetTransaction_ReturnsNOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.GetTransaction(1);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Post_ReturnsNOK()
        {
            var mockRepo = new Mock<ITransactionManipulation>();
            var mockSub = new Mock<ISubscriptionManipulation>();
            var controller = new TransactionController(mockRepo.Object, mockSub.Object);
            // Act
            TransactionDto transaction = new TransactionDto();
            var result = controller.Post(transaction);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetTransactions_ReturnsOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.GetTransactions();
            // Assert
            Assert.NotEmpty(result);
        }


        [Fact]
        public void GetTransactions_ReturnsNOK()
        {
            var mockRepo = new Mock<ITransactionManipulation>();
            var mockSub = new Mock<ISubscriptionManipulation>();
            var controller = new TransactionController(mockRepo.Object, mockSub.Object);
            // Act
            var result = controller.GetTransactions();
            // Assert
            Assert.IsType<System.Collections.Generic.List<TransactionDto>>(result);
        }

        [Fact]
        public void GetTransactionsByCustomer_ReturnsOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.GetTransactionsByCustomer(1);
            // Assert
            Assert.IsType<System.Collections.Generic.List<TransactionDto>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetTransactionsByCustomer_ReturnsNOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.GetTransactionsByCustomer(2);
            // Assert
            Assert.IsType<System.Collections.Generic.List<TransactionDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void CreateClientPaypalToken_ReturnOK()
        {
            var controller = new TransactionController(itm, ism);
            // Act
            var result = controller.CreateClientPaypalToken();
            Assert.NotEmpty(result);
        }

       
    }
}