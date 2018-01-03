using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.ContactsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests
{
    public class ContactsControllerTest
    {
        IkarusContext db = new IkarusContext();
        IContactsRepository contactsRepository => new ContactsRepository(db);
        IContactsManipulation contactsManipulation => new ContactsManipulation(contactsRepository);
        [Fact]
        public void GetCasesByContact_ReturnsOK()
        {
            var controller = new ContactsController(contactsManipulation);

            // Act
            var result = controller.GetByCase(25);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            var mockRepo = new Mock<IContactsManipulation>();
            var controller = new ContactsController(mockRepo.Object);
            int id = 100;
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Put(id, model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void UpdateContact_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1;
            var contact = new ContactDto()
            {
                Contact1 = id,
                AddressId = 1,
                CreatedByUserId = 6,
                FirsttName = "Contactsfirstname",
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1
            };

            var controller = new ContactsController(this.contactsManipulation);

            // Act
            var result = controller.Put(id, contact);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IContactsManipulation>();
            var controller = new ContactsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Delete(500);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void Delete_ReturnsOK()
        {
            // Arrange
            var contact = new ContactDto()
            {
                AddressId = 1,
                CreatedByUserId = 6,
                FirsttName = "Contactsfirstname",
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1
            };
            var controller = new ContactsController(this.contactsManipulation);
            db.Contact.Add(NSI.Repository.Mappers.ContactRepository.MapToDbEntity(contact));
            // Act
            var result = controller.Delete(contact.Contact1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
