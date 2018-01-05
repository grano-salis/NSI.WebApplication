using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using NSI.REST.Models;
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
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
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
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Put(id, meeting);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_ThrowsAnException()
        {
            // Arrange
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);


            // Act
            var result = controller.Put(-1, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetAllMeetings_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetMeetingById_ReturnsNoContent()
        {
            // Arrange
            var meetingRepo = new Mock<IMeetingsRepository>();

            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Get(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetMeetingById_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.Get(123);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void Delete_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IMeetingsManipulation>();
            var controller = new MeetingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.DeleteMeeting(100);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void DeleteMeeting_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.DeleteMeeting(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Delete_ThrowsAnException()
        {
            // Arrange
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);


            // Act
            var result = controller.DeleteMeeting(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetMeetingByUserId_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetingsByUser(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void GetMeetingByUserId_ReturnsBadRequest()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetingsByUser(-11);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void GetMeetingTimes_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddDays(10);

            var usersOnMeeting = new List<UserMeetingDto>()
                {
                    new UserMeetingDto()
                    {
                        UserId = 1
                    },
                    new UserMeetingDto()
                    {
                        UserId = 2
                    },
                    new UserMeetingDto()
                    {
                        UserId = 3
                    },
                    new UserMeetingDto()
                    {
                        UserId = 4
                    }
                };

            var meeting = new MeetingDto()
            {
                MeetingId = id,
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };
            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetingsTimes(new List<int> { 1, 2, 3, 4 }, from, to, 10, 123);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetMeetingTimes_UserIdReturnsBadRequest()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetingsTimes(new List<int> { -1, 2, 3, 4 }, DateTime.Now, DateTime.Now, 10, 10);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetMeetingTimes_MeetingIdReturnsBadRequest()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetingsTimes(new List<int> { 1, 2, 3, 4 }, DateTime.Now, DateTime.Now, 10, -1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CheckUsersAvailability_ReturnsOK()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.CheckUsersAvailability(new List<int> { 1, 2, 3, 4 }, DateTime.Now, DateTime.Now, 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CheckUsersAvailability_UserIdReturnsBadRequest()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.CheckUsersAvailability(new List<int> { -1, 2, 3, 4 }, DateTime.Now, DateTime.Now, 10);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CheckUsersAvailability_MeetingIdReturnsBadRequest()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.CheckUsersAvailability(new List<int> { 1, 2, 3, 4 }, DateTime.Now, DateTime.Now, -1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void Search_ThrowsException()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddDays(10);

            var usersOnMeeting = new List<UserMeetingDto>()
                {
                    new UserMeetingDto()
                    {
                        UserId = 1
                    }
                };

            var meeting = new MeetingsSearchModel()
            {
                MeetingId = id,
                Title = title,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            var result = controller.Search(meeting, 1, 1);
            // Assert
            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public void Search_ReturnsOK()
        {
            //ToBeImplemented
        }
        [Fact]
        public void Search_ThrowsNSIException()
        {
            var meetingRepo = new Mock<IMeetingsRepository>();
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            var result = controller.Search(null, 1, 1);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetMeetings_ReturnsOK()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetings(null, null);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetMeetings_ThrowsException()
        {
            // Arrange
            int id = 123;
            string title = "naziv";
            string meetPlace = "mjesto";
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
                Title = title,
                MeetingPlace = meetPlace,
                From = from,
                To = to,
                UserMeeting = usersOnMeeting
            };

            var meetingRepo = new Mock<IMeetingsRepository>();
            meetingRepo.Setup(x => x.InsertMeeting(meeting));
            var meetingManipulation = new MeetingsManipulation(meetingRepo.Object);
            var controller = new MeetingsController(meetingManipulation);

            // Act
            var result = controller.GetMeetings(1, 1);

            // Assert
            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public void GetMeetings_ThrowsNSIException()
        {
            //ToBeImplemented
        }
    }
}