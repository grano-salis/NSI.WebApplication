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


        #region getTasks
        [Fact]
        public void getAlTasks_ReturnsNoContent()
        {
            var controller = new TasksController(itm);
            var result = controller.GetTasks(-1, null);
            Assert.IsType<OkObjectResult>(result);            

        }

        [Fact]
        public void getAllTasks()
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

            var taskMan = new Mock<ITaskManipulation>();
            var controller = new TasksController(taskMan.Object);

            var result = controller.GetTasks(21, 1);
            Assert.IsType<OkObjectResult>(result);

        }
        #endregion

        #region getTasksByID
        [Fact]
        public void getTasksByIDTest_ReturnOK()
        {
            var controller = new TasksController(itm);
            var result = controller.GetTaskById(3);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void getTasksByIDTest_ReturnsNoContent()
        {
            var TaskRepo = new Mock<ITaskRepository>();

            var TaskManipulation = new TaskManipulation(TaskRepo.Object);
            var controller = new TasksController(TaskManipulation);
            
            var result = controller.GetTaskById(-1);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region getTaskByUserID
        [Fact]
        public void getTasksByUserId_ReturnOK()
        {
            int id = 21;
            string description = "Test";
            DateTime dueDate = DateTime.Now;
            String title = "Test Task";

            var userOnTask = new List<TaskDto>()
            {
                 new TaskDto()
                 {
                     UserId = 69                     
                 }
             };

            NSI.REST.Models.TasksCreateModel task = new TasksCreateModel()
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title,
                UserId = 69
            };

            var task2 = new TaskDto
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title,
                UserId = 69

            };

            var taskMan = new Mock<ITaskManipulation>(); 
            var controller = new TasksController(taskMan.Object);

            var result = controller.GetTasksByUserId(69,3,1);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void getTasksByUserId_ReturnsBadRequest()
        {
            var taskRepo = new Mock<ITaskRepository>();
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);

            var result = controller.GetTasksByUserId(1, null, null);

            Assert.IsType<OkObjectResult>(result);

        }
        #endregion


        #region CreateTask
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

        #endregion

        #region UpdateTask

        [Fact]
        public void Update_ReturnsBadRequest_GivenInvalidModel() //put
        {
            var mockRepo = new Mock<ITaskManipulation>();
            var controller = new TasksController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            var result = controller.ChangeTask(6,model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateTask_ReturnsOK()
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

        [Fact]
        public void UpdateTask_ThrowException()
        {
            var TaskRepo = new Mock<ITaskRepository>();
            var TaskManipulation = new TaskManipulation(TaskRepo.Object);
            var controller = new TasksController(TaskManipulation);

            var result = controller.ChangeTask(5,model: null);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region deleteTask

        [Fact]

        public void deleteTask_ReturnsOK()
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

            var result = controller.DeleteTask(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void deleteTask_ReturnBadRequest_GivenInvalidMove()
        {
            var taskRepo = new Mock<ITaskManipulation>();
            var controller = new TasksController(taskRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            var result = controller.DeleteTask(-1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void deleteTask_throwException()
        {
            var taskRepo = new Mock<ITaskRepository>();
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);

            var result = controller.DeleteTask(-1);
            
            Assert.IsType<OkObjectResult>(result);
        }

        #endregion

        #region SearchTask

        [Fact]
        public void searchTask_returnOK()
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

            var taskSearchModel = new TasksSearchModel()
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title

            };


            var taskMan = new Mock<ITaskManipulation>();
            var controller = new TasksController(taskMan.Object);

            var result = controller.Search(taskSearchModel, 1, 1);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void search_throwException()
        {

            int id = -21;
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

            var taskSearchModel = new TasksSearchModel()
            {
                TaskId = id,
                Description = description,
                DueDate = dueDate,
                Title = title

            };

            var taskRepo = new Mock<ITaskRepository>();
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);


            var result = controller.Search(taskSearchModel, null, null);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Search_ThrowsNSIException()
        {
            var taskRepo = new Mock<ITaskRepository>();
            var taskManipulation = new TaskManipulation(taskRepo.Object);
            var controller = new TasksController(taskManipulation);

            var result = controller.Search(null, 1, 1);
           
            Assert.IsType<OkObjectResult>(result);

        }

        #endregion

        #region GetTaskWithDueDateRange

        [Fact]
        public void GetTaskWithDueDateRange_ReturnOK() //dateFrom, dateTo, page,pageSize
        {
            int id = 21;
            string description = "Test";
            DateTime dueDateFrom = DateTime.Now;
            DateTime dueDateTo = DateTime.Now;
            dueDateTo.AddYears(1);
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
                DueDate = dueDateFrom,
                Title = title

            };

            var task2 = new TaskDto
            {
                TaskId = id,
                Description = description,
                DueDate = dueDateFrom,
                Title = title


            };
            var taskMan = new Mock<ITaskManipulation>();
            var controller = new TasksController(taskMan.Object);

            var result = controller.GetTasksWithDueDateRange(dueDateFrom, dueDateTo, 1, 1);
            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public void GetTaskWithDueDateRange_ReturnInvalidMode()
        {
            var taskRepo = new Mock<ITaskManipulation>();
            var controller = new TasksController(taskRepo.Object);
            controller.ModelState.AddModelError("error", "some error");
            DateTime dateFrom = DateTime.Now;
            DateTime dateTo = DateTime.Now;
            dateTo.AddYears(-20);
            var result = controller.GetTasksWithDueDateRange(dateFrom, dateTo, 1, 1);
            Assert.IsType<OkObjectResult>(result);
        }


        #endregion

    }
}
