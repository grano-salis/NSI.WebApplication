using IkarusEntities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSI.BLL;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using NSI.DC.TaskRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
using NSI.REST.Controllers;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests
{
    public class TaskControllerTest
    {
        IkarusContext db = new IkarusContext();
        ITaskRepository itr => new TaskRepository(db);
        ITaskManipulation itm => new TaskManipulation(itr);
        
        [Fact]
        public void getWithPagingTest ()
        {
            var controller = new TasksController(itm);
            var result = controller.GetTasks(3, 1); 
            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public void getWithPagingTest_ReturnsNoContent()
        {
            var controller = new TasksController(itm);
            var result = controller.GetTasks(3, 1);
            Assert.IsType<OkObjectResult>(result);
            

        }

        [Fact]
        public void getTest()
        {
            var controller = new TasksController(itm);
            var result = controller.GetTaskById(3);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            
            var mockRepo = new Mock<ITaskManipulation>();
            var controller = new TasksController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.InsertTask(model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void Create_ReturnsNewlyCreatedTask()
        {
            int id = 21;
            string description = "Test";
            DateTime dueDate= DateTime.Now;
            String title = "Test Task";




            var Task = new List<TaskDto>()
            {
                 new TaskDto()
                 {
                     TaskId = 1
            
                 }
             };

            NSI.REST.Models.TasksCreateModel task = new TasksCreateModel()
            {
                  TaskId = id,
                  Description = description,
                  DueDate = dueDate,
                  Title = title
                
            };

            var task2 = new TaskDto
            {
                TaskId = id,
                  Description = description,
                  DueDate = dueDate,
                  Title = title


            };

            var taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(x => x.CreateTask(task2));
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);
            var result = controller.InsertTask(task);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Put_ReturnsBadRequest_GivenInvalidModel()
        {
            var mockRepo = new Mock<ITaskManipulation>();
            var controller = new TasksController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.ChangeTask(6,model: null);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateMeeting_ReturnsOK()
        {                    
            int id = 21;
            string description = "Test";
            DateTime dueDate = DateTime.Now;
            String title = "Test Task";




            var Task = new List<TaskDto>()
            {
                 new TaskDto()
                 {
                     TaskId = 1

                 }
             };

            NSI.REST.Models.TasksEditModel task = new TasksEditModel()
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title

            };

            var task2 = new TaskDto
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title


            };

            var taskRepo = new Mock<ITaskRepository>();
            taskRepo.Setup(x => x.EditTask(id,task2));
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);
            var result = controller.ChangeTask(id,task);
            Assert.IsType<OkObjectResult>(result);
        }



    }
}
