using IkarusEntities;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NSI.Repository
{
    public partial class TaskRepository
    {
        private readonly IkarusContext _dbContext;

        public TaskRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TaskDto CreateTask(TaskDto taskDto) {
            var task = Mappers.TaskRepository.MapToDbEntity(taskDto);
            _dbContext.Add(task);
            if (_dbContext.SaveChanges() != 0)
                return Mappers.TaskRepository.MapToDto(task);
            return null;

        }
        //Not tested
        public TaskDto GetTaskById(int taskId) {
            var task = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
            if (task != null)
            {
                return Mappers.TaskRepository.MapToDto(task);
            }
            return null;
        }
        //Not tested
        public ICollection<TaskDto> GetTasks()
        {
            var tasks = _dbContext.Task;
            if (tasks != null)
            {
                ICollection<TaskDto> tasksDto = new List<TaskDto>();
                foreach (var item in tasks)
                {
                    tasksDto.Add(Mappers.TaskRepository.MapToDto(item));
                }
                return tasksDto;
            }
            return null;
        }
        //Not tested
        public bool DeleteTaskById(int taskId) {
            var task = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
            if (task != null)
            {
                if (_dbContext.Task.Remove(task) != null)
                {
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public ICollection<TaskDto> SearchTasks(TaskDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }


            return null;

         }
    }
}
