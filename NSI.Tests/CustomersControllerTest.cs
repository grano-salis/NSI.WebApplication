using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.CustomersRepository;
using NSI.DC.Exceptions;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests
{
    public class CustomersControllerTest
    {
        IkarusContext db = new IkarusContext();
        ICustomerRepository customersRepository => new CustomerRepository(db);
        ICustomerManipulation customersManipulation => new CustomerManipulation(customersRepository);

        [Fact]
        public void GetAllCustomers_ReturnsOK()
        {
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetAllCustomers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerById_ReturnsOK()
        {
            // Arrange
            int id = 1;
            var customer = new CustomerDto()
            {
                AddressId = 1,
                PricingPackageId = 1,
                CustomerName = "firstname"
            };
            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.GetCustomerById(It.IsAny<int>())).Returns(customer);
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetCustomerById(1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerById_ReturnsNotFound_GivenBadId()
        {
            // Arrange
            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.GetCustomerById(It.IsAny<int>())).Returns((CustomerDto)null);
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetCustomerById(1);
            
            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void CreateCustomerBadRequest()
        {
            // Arrange
            int id = 1;
            var customer = new CustomerDto()
            {
                AddressId = 1,
                PricingPackageId = 1,
                CustomerName = "firstname"
            };

            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.CreateCustomer(It.IsAny<CustomerDto>())).Throws<NSIException>();
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);
            var result = controller.CreateNewCustomer(customer);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateCustomer_ReturnsOk()
        {
            // Arrange
            int id = 1;
            var customer = new CustomerDto()
            {
                AddressId = 1,
                PricingPackageId = 1,
                CustomerName = "firstname"
            };

            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.CreateCustomer(It.IsAny<CustomerDto>())).Returns(customer);
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);
            var result = controller.CreateNewCustomer(customer);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteCustomer_ReturnsOk()
        {
            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.DeleteCustomerById(It.IsAny<int>())).Returns(true);
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);
            int id = 100;

            // Act
            var result = controller.DeleteCustomer(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteCustomer_ReturnsNotFound_GivenBadId()
        {
            var customersRepo = new Mock<ICustomerRepository>();
            customersRepo.Setup(x => x.DeleteCustomerById(It.IsAny<int>())).Returns(false);
            var customersManipulation = new CustomerManipulation(customersRepo.Object);
            var controller = new CustomersController(customersManipulation);
            int id = 100;

            // Act
            var result = controller.DeleteCustomer(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateCustomer_ReturnsOk()
        {
            // Arrange
            int id = 1;
            var customer = new CustomerDto()
            {
                AddressId = 1,
                PricingPackageId = 1,
                CustomerName = "firstname"
            };

            // Act
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(x => x.CreateCustomer(It.IsAny<CustomerDto>())).Returns(customer);
            mockRepo.Setup(x => x.EditCustomer(id ,It.IsAny<CustomerDto>())).Returns(true);
            var customerManipulation = new CustomerManipulation(mockRepo.Object);
            var controller = new CustomersController(customerManipulation);
            controller.CreateNewCustomer(customer);

            //update attributes

            customer.CustomerName = "Johndoe";

            var result = controller.EditCustomer(id,customer);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerClients_ReturnsOK()
        {
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetCustomerClients();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerCasesYearly_ReturnsOK()
        {
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetCustomerCasesYearly(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerCasesMonthly_ReturnsOK()
        {
            var controller = new CustomersController(customersManipulation);

            // Act
            var result = controller.GetCustomerCasesMonthly(1, 2017);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
       
    }
}
