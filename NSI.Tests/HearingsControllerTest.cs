using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
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
        public void Create_ReturnsNewlyCreatedMeeting()
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
            hearingRepo.Setup(x => x.Insert(hearing));
            var hearingManipulation = new HearingsManipulation(hearingRepo.Object);


            var controller = new HearingsController(hearingManipulation);

            // Act
            var result = controller.Post(hearing);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
