using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL;
using NSI.DC.TaskRepository;
using NSI.BLL.Interfaces;
using NSI.Repository;
using System.Net.Http;
using System.Net;
using NSI.REST.Models;
using NSI.REST.Middleware;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        ITaskManipulation _taskRepository { get; set; }

        public TasksController(ITaskManipulation taskManipulation)
        {
            _taskRepository = taskManipulation;
        }

        // GET: api/Tasks
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_taskRepository.GetTasks());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getWithPaging")]
        public IActionResult GetWithPaging(int pageNumber, int pageSize)
        {
            try
            {
                return Ok(_taskRepository.GetTasks(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_taskRepository.GetTaksById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult Post([FromBody]TasksCreateModel model)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskDto taskDto = new TaskDto()
            {
                Description=model.Description,
                DueDate=model.DueDate,
                Title=model.Title,
                UserId=model.UserId
            };

            try
            {
                var task = _taskRepository.CreateTask(taskDto);
                if (task != null)
                    return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TasksEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskDto taskDto = new TaskDto()
            {
                Description = model.Description,
                DueDate = model.DueDate,
                Title = model.Title,
                UserId = model.UserId
            };

            try
            {

                if (_taskRepository.EditTask(id, taskDto))
                    return Ok();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_taskRepository.DeleteTaskById(id))
                    return Ok();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromBody]TasksSearchModel model, int pageNumber, int pageSize)
        {
            try
            {
                TaskSearchCriteriaDto taskDto = new TaskSearchCriteriaDto()
                {
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Title = model.Title,
                    UserId = model.UserId,
                    TaskId=model.TaskId ?? 0
                };

                return Ok(_taskRepository.SearchTasks(taskDto, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
