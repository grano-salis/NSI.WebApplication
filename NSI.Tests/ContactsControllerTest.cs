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
        public void UpdateContactBadRequestFailureTest()
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

            var contactsRepo = new Mock<IContactsRepository>();
            contactsRepo.Setup(x => x.EditContactById(It.IsAny<int>(), It.IsAny<ContactDto>())).Throws<Exception>();
            var contactsManipulation = new ContactsManipulation(contactsRepo.Object);
            var controller = new ContactsController(contactsManipulation);
            var result = controller.Put(1, contact);
            Assert.IsType<BadRequestObjectResult>(result);
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
        public void UpdateContact_ReturnsOkResult()
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


            // Act
            var mockRepo = new Mock<NSI.Repository.Interfaces.IContactsRepository>();
            mockRepo.Setup(x => x.CreateContact(It.IsAny<ContactDto>(), id)).Returns(contact);
            mockRepo.Setup(x => x.EditContactById(id, It.IsAny<ContactDto>())).Returns(true);
            var contManipulation = new ContactsManipulation(mockRepo.Object);
            var controller = new ContactsController(contManipulation);
            controller.Post(1, contact);

            //update attributes

            contact.FirsttName = "Johndoe";
            contact.LastName = "Doeeon";

            var result = controller.Put(id, contact);

            // Assert
            Assert.IsType<OkObjectResult>(result);
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


            // Act
            var mockRepo = new Mock<NSI.Repository.Interfaces.IContactsRepository>();
            mockRepo.Setup(x => x.CreateContact(It.IsAny<ContactDto>(), id)).Returns(contact);
            mockRepo.Setup(x => x.EditContactById(id, It.IsAny<ContactDto>())).Returns(false);
            var contManipulation = new ContactsManipulation(mockRepo.Object);
            var controller = new ContactsController(contManipulation);
            controller.Post(1, contact);

            //update attributes

            contact.FirsttName = "Johndoe";
            contact.LastName = "Doeeon";

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
            int id = 100;
            // Arrange
            var contact = new ContactDto()
            {
                AddressId = 1,
                CreatedByUserId = 6,
                FirsttName = "Contactsfirstname",
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1,
            };
            // Act
            var mockRepo = new Mock<NSI.Repository.Interfaces.IContactsRepository>();
            mockRepo.Setup(x => x.CreateContact(It.IsAny<ContactDto>(), 1)).Returns(contact);
            mockRepo.Setup(x => x.DeleteContactById(It.IsAny<int>())).Returns(true);
            var contManipulation = new ContactsManipulation(mockRepo.Object);
            var controller = new ContactsController(contManipulation);
            controller.Post(1, contact);
            var result = controller.Delete(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void DeleteAddressBadRequestFailureTest()
        {
            int id = 100;
            // Arrange
            var contact = new ContactDto()
            {
                AddressId = 1,
                CreatedByUserId = 6,
                FirsttName = "Contactsfirstname",
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1,
            };

            var contactsRepo = new Mock<IContactsRepository>();
            contactsRepo.Setup(x => x.DeleteContactById(It.IsAny<int>())).Throws<Exception>();
            var contactsManip = new ContactsManipulation(contactsRepo.Object);
            var controller = new ContactsController(contactsManip);
            var result = controller.Delete(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ContactsGetAll()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            var contacts = controller.Get(10000,1, "", "", "",0);

            // Assert
            Assert.IsType<OkObjectResult>(contacts);
        }

        [Fact]
        public void ContactsGetById()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            
            var contacts = ((controller.Get(10000, 1, "", "", "",0) as OkObjectResult).Value as NSI.DC.ContactsRepository.PaggedContactDto);
            if (contacts != null && contacts.Total > 0)
            {
                var contact = (contacts.Contacts as List<ContactDto>)[0];
                var result = controller.Get(contact.Contact1);
                Assert.IsType<OkObjectResult>(result);

            }
            else Assert.IsType<NoContentResult>(new NoContentResult());
        }


        [Fact]
        public void AddContactOk()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
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
            var result = controller.Post(0,contact);
            Assert.IsType<OkObjectResult>(result);
           
        }

        [Fact]
        public void AddContactBadModel()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            var contact = new ContactDto()
            {
                AddressId = 1,
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1
            };
            var result = controller.Post(0,contact);
            Assert.IsType<BadRequestObjectResult>(result);

        }


        [Fact]
        public void AddContactBadValidation()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            var contact = new ContactDto()
            {
                AddressId = 1,
                CreatedByUserId = 6,
                FirsttName = "12345",
                LastName = "Contactslastname",
                Phones = new List<PhoneDto>(),
                Emails = new List<EmailDto>(),
                TaskId = 1
            };
            var result = controller.Post(0,contact);
            Assert.IsType<BadRequestObjectResult>(result);

        }


        [Fact]
        public void AddContactEmptyModel()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            var result = controller.Post(0,null);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void AddContactEmptyModelState()
        {
            // Arrange
            var controller = new ContactsController(this.contactsManipulation);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.Post(0,new ContactDto());
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}
