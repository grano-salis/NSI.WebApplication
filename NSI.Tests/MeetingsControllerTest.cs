using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class MeetingsControllerTest
    {
        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IMeetingsManipulation>();
            var controller = new MeetingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void Create_ReturnsNewlyCreatedMeeting()
        {
            // Arrange
            int id = 123;
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddDays(10);

            var usersOnMeeting = new List<UserMeetingDto>()
                {
                    new UserMeetingDto()
                    {
                        UserId = 1
                    }
                };

            var meeting = new MeetingDto()
            {
                MeetingId = id,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.Insert(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);


            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Post(meeting);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IMeetingsManipulation>();
            var controller = new MeetingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Put(5, model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateMeeting_ReturnsOK()
        {
            // Arrange
            int id = 123;
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddDays(10);

            var usersOnMeeting = new List<UserMeetingDto>()
                {
                    new UserMeetingDto()
                    {
                        UserId = 1
                    }
                };

            var meeting = new MeetingDto()
            {
                MeetingId = id,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.Insert(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Put(id, meeting);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        IkarusContext db = new IkarusContext();
        IMeetingsRepository imr => new MeetingsRepository(db);
        IMeetingsManipulation imm => new MeetingsManipulation(imr);
        [Fact]
        public void GetAllMeetings_ReturnsOK()
        {
            // Arrange & Act
            
            var controller = new MeetingsController(imm);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetMeetingById_ReturnsNoContent()
        {
            var controller = new MeetingsController(imm);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void GetMeetingById_ReturnsOK()
        {
            var controller = new MeetingsController(imm);

            // Act
            var result = controller.Get(21);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

    }
}
