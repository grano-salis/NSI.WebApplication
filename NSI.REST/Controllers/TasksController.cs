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

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        ITaskManipulation _taskRepository { get; set; }

        public TasksController()
        {
            _taskRepository = new TaskManipulation();
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<TaskDto> Get()
        {
           return _taskRepository.GetTasks();

        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public TaskDto Get(int id)
        {
            return _taskRepository.GetTaksById(id);
        }
        
        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
