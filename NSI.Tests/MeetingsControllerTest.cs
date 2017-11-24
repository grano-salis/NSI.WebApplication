using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using NSI.REST.Controllers;
using System;
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
       
            var mockRepo = new Mock<IMeetingsManipulation>();
            var controller = new MeetingsController(mockRepo.Object);

            // Act
            var result = controller.Post(new MeetingDto()
            {
                MeetingId = id,
                From = from,
                To = to
            });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<MeetingDto>(okResult.Value);
            mockRepo.Verify();
            Assert.Equal(id, returned.MeetingId);
            Assert.Equal(from, returned.From);
            Assert.Equal(to, returned.To);
        }
    }
}
