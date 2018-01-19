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
using NSI.DC.Exceptions;
using NSI.DC.Response;

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
		[ProducesResponseType(typeof(NSIResponse<ICollection<TaskDto>>), 200)]
		public IActionResult GetTasks([FromQuery] int? page, [FromQuery] int? pageSize)
		{
			try
			{
				return Ok(new NSIResponse<ICollection<TaskDto>> { Data = _taskRepository.GetTasks(page, pageSize), Message = "Success" });
			}
			catch (Exception ex)
			{
				Logger.Logger.LogError(ex);
				return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
			}
		}

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetTaskById([FromRoute] int id)
        {
            try
            {
                return Ok(new NSIResponse<TaskDto> { Data = _taskRepository.GetTaksById(id), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult InsertTask([FromBody]TasksCreateModel model)
        {
            try
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

                return Ok(new NSIResponse<TaskDto> { Data = _taskRepository.CreateTask(taskDto), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message= "Parameter error!"});
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult ChangeTask(int id, [FromBody]TasksEditModel model)
        {
            try
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
                    UserId = model.UserId,
                    Status = model.Status,
                    DateModified = DateTime.Now
                };

                return Ok(new NSIResponse<TaskDto> { Data = _taskRepository.EditTask(id, taskDto), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                if (_taskRepository.DeleteTaskById(id))
                    return Ok(new NSIResponse<object> { Data = null, Message = "Success" });
                return Ok(new NSIResponse<object> { Data = null, Message= "Failed" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("search")]
        public IActionResult Search([FromBody]TasksSearchModel model, int? pageNumber, int? pageSize)
        {
            try
            {
                if (model == null)
                    throw new NSIException("TaskSearchModel is null", DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);

                TaskSearchCriteriaDto taskDto = new TaskSearchCriteriaDto()
                {
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Title = model.Title,
                    UserId = model.UserId,
                    TaskId=model.TaskId ?? 0
                };

                return Ok(new NSIResponse<ICollection<TaskDto>> { Data = _taskRepository.SearchTasks(taskDto, pageNumber, pageSize), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message= "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(NSIResponse<ICollection<TaskDto>>), 200)]
        [Route("getTasks/{userId}")]
        public IActionResult GetTasksByUserId(int userId, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                return Ok(new NSIResponse<ICollection<TaskDto>> { Data = _taskRepository.GetTasksByUserId(userId, page, pageSize), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

		/*[HttpGet]
		[ProducesResponseType(typeof(NSIResponse<ICollection<TaskDto>>), 200)]
		[Route("getTasks/{userId}")]
		public IActionResult GetTasksByUserId(int userId, [FromQuery] int? page, [FromQuery] int? pageSize)
		{
			try
			{
				return Ok(new NSIResponse<ICollection<TaskDto>> { Data = _taskRepository.GetTasksByUserId(userId, page, pageSize), Message = "Success" });
			}
			catch (NSIException ex)
			{
				Logger.Logger.LogError(ex);
				if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
					return NoContent();
				return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
			}
			catch (Exception ex)
			{
				Logger.Logger.LogError(ex);
				return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
			}
		}*/

		[HttpGet]
		[ProducesResponseType(typeof(NSIResponse<ICollection<TaskDto>>), 200)]
		[Route("getTasks/{username}")]
		public IActionResult GetTasksByUsername(string username, [FromQuery] int? page, [FromQuery] int? pageSize)
		{
			try
			{
				return Ok(new NSIResponse<ICollection<TaskDto>> { Data = _taskRepository.GetTasksByUsername(username, page, pageSize), Message = "Success" });
			}
			catch (NSIException ex)
			{
				Logger.Logger.LogError(ex);
				if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
					return NoContent();
				return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
			}
			catch (Exception ex)
			{
				Logger.Logger.LogError(ex);
				return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
			}
		}

		[HttpGet]
		[ProducesResponseType(typeof(NSIResponse<ICollection<TaskDto>>), 200)]
		[Route("getTasksWithDueDateRange")]
		public IActionResult GetTasksWithDueDateRange([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int? page, [FromQuery] int? pageSize)
		{
			try
			{
				if (dateFrom > dateTo)
					return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
				dateFrom = dateFrom ?? DateTime.Now.AddDays(-2);
				dateTo = dateTo ?? DateTime.Now.AddDays(2);
				int count = _taskRepository.GetTasksWithDueDateRangeCount(DateTime.Now.AddYears(-20), (DateTime)dateTo);
				return Ok(new NSIResponse<ICollection<TaskDto>> { Count = count, Data = _taskRepository.GetTasksWithDueDateRange((DateTime)dateFrom, (DateTime)dateTo, page, pageSize), Message = "Success" });
			}
			catch (NSIException ex)
			{
				Logger.Logger.LogError(ex);
				if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
					return NoContent();
				return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
			}
			catch (Exception ex)
			{
				Logger.Logger.LogError(ex);
				return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
			}
		}
	}
}
