using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.Repository.Repository;
using NSI.REST.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class HearingsControllerTest
    {
        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IHearingsManipulation>();
            var controller = new HearingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Post(model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Create_ReturnsNewlyCreatedHearing()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.Post(hearing);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IHearingsManipulation>();
            var controller = new HearingsController(mockRepo.Object);
            int id = 123;
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Put(id, model: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Update_ReturnsUpdatedHearing()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var newNotes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                },
                new NoteDto
                {
                    Text = "new note",
                    CreatedByUserId = 2,
                    HearingId = 123
                },
                new NoteDto
                {
                    Text = "new note2",
                    CreatedByUserId = 3,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var resultCreated = controller.Post(hearing);

            hearing.HearingDate = DateTime.Now.AddDays(15);
            hearing.Note = newNotes;

            var resultUpdated = controller.Put(id, hearing);

            // Assert
            Assert.IsType<OkObjectResult>(resultCreated);
            Assert.IsType<OkObjectResult>(resultUpdated);
        }

        [Fact]
        public void GetHearingsByCase_ReturnsCase()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.GetHearingsByCase(3);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetHearingsByCase_ReturnsBadRequest()
        {
            //Arrange
            var hearingRepo = new Mock<IHearingsRepository>();
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);
            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.GetHearingsByCase(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAll_ReturnsAllHearings()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetHearingById_ReturnsBadRequest()
        {
            // Arrange
            var hearingRepo = new Mock<IHearingsRepository>();
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);
            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.Get(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetHearingById_ReturnsOK()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.Get(123);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void Delete_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var mockRepo = new Mock<IHearingsManipulation>();
            var controller = new HearingsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.DeleteHearing(100);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void Delete_ReturnsOK()
        {
            // Arrange
            int id = 123;
            DateTime hearingDate = DateTime.Now;

            int createdByUserId = 1;
            int caseId = 3;

            var usersOnHearing = new List<UserHearingDto>()
            {
                new UserHearingDto()
                {
                    UserId = 1
                }
            };

            var notes = new List<NoteDto>()
            {
                new NoteDto
                {
                    Text = "test test",
                    CreatedByUserId = 1,
                    HearingId = 123
                }
            };

            var hearing = new HearingDto()
            {
                HearingId = id,
                HearingDate = hearingDate,
                CreatedByUserId = createdByUserId,
                CaseId = caseId,
                UserHearing = usersOnHearing,
                Note = notes
            };

            var hearingRepo = new Mock<IHearingsRepository>();
            hearingRepo.Setup(x => x.InsertHearing(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.DeleteHearing(3);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Delete_ReturnsBadRequest()
        {
            //Arrange
            var hearingRepo = new Mock<IHearingsRepository>();

            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);
            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.DeleteHearing(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}
