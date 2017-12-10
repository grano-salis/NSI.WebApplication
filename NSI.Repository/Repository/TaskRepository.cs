using IkarusEntities;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NSI.Repository.Interfaces;
using NSI.DC.Exceptions;

namespace NSI.Repository
{
    public partial class TaskRepository:ITaskRepository
    {
        private readonly IkarusContext _dbContext;

        public TaskRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TaskDto CreateTask(TaskDto taskDto) {
            try
            {
                var task = Mappers.TaskRepository.MapToDbEntity(taskDto);
                _dbContext.Add(task);
                if (_dbContext.SaveChanges() != 0)
                    return Mappers.TaskRepository.MapToDto(task);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }
            return null;

        }

        public TaskDto GetTaskById(int taskId) {
            try
            {
                var task = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
                if (task != null)
                {
                    return Mappers.TaskRepository.MapToDto(task);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!"); 
            }
            return null;
        }

        public ICollection<TaskDto> GetTasks()
        {
            try
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
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }
            return null;
        }

        public bool DeleteTaskById(int taskId) {
            try
            {
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
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }
        }

        public ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                Logger.Logger.LogError("SearchTasks searchCriteria is null!");
                throw new ArgumentNullException("SearchTasks searchCriteria");
            }
            var tasks = from task in _dbContext.Task select task;

            if (!string.IsNullOrEmpty(searchCriteria.Description))
                tasks = tasks.Where(x => x.Description.Contains(searchCriteria.Description));

            if (!string.IsNullOrEmpty(searchCriteria.Title))
                tasks = tasks.Where(x => x.Title.Contains(searchCriteria.Title));

            if (searchCriteria.UserId!=0)
                tasks = tasks.Where(x => x.UserId==searchCriteria.UserId);

            if (searchCriteria.DueDate!=null)
                tasks = tasks.Where(x => x.DueDate.Value.Date == searchCriteria.DueDate.Value.Date);

            ICollection<TaskDto> tasksDto = new List<TaskDto>();
            foreach (var item in tasks)
            {
                tasksDto.Add(Mappers.TaskRepository.MapToDto(item));
            }
            return tasksDto;
        }

        public bool EditTask(int taskId, TaskDto task)
        {
            try
            {
                var taskTmp = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
                if (taskTmp != null)
                {
                    taskTmp.Description = task.Description ?? taskTmp.Description;
                    taskTmp.DueDate = task.DueDate != null ? task.DueDate : taskTmp.DueDate;
                    taskTmp.Title = task.Title ?? taskTmp.Title;
                    taskTmp.UserId = task.UserId != 0 ? task.UserId : taskTmp.UserId;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }
        }
    }
}
