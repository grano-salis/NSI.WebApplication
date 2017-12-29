using IkarusEntities;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NSI.Repository.Interfaces;
using NSI.DC.Exceptions;
using NSI.DC.Exceptions.Enums;

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

            if (taskDto == null)
                throw new NSIException("Parameter taskDto is null!", Level.Error, ErrorType.InvalidParameter);

            var task = Mappers.TaskRepository.MapToDbEntity(taskDto);
            _dbContext.Add(task);
            if (_dbContext.SaveChanges() != 0)
                return Mappers.TaskRepository.MapToDto(task);

            throw new NSIException("No data in database!", Level.Info, ErrorType.MissingData);
        }

        public TaskDto GetTaskById(int taskId) {
            if (taskId == 0)
                throw new NSIException("Parameter taskId is invalid!", Level.Error, ErrorType.InvalidParameter);

            var task = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
            if (task != null)
            {
                return Mappers.TaskRepository.MapToDto(task);
            }

            throw new NSIException("No result for parameter taskId=" + taskId.ToString(), Level.Info, ErrorType.MissingData);
        }

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
            throw new NSIException("No data in database!", Level.Info, ErrorType.MissingData);
        }

        public bool DeleteTaskById(int taskId) {
            if (taskId == 0)
                throw new NSIException("Parameter taskId is invalid!", Level.Error, ErrorType.InvalidParameter);

            var task = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
            if (task != null)
            {
                if (_dbContext.Task.Remove(task) != null)
                {
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }

            throw new NSIException("No data for parameter taskId="+taskId.ToString(), Level.Info, ErrorType.MissingData);
        }

        public ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                Logger.Logger.LogError("SearchTasks searchCriteria is null!");
                throw new NSIException("Parameter searchCriteria is null!", Level.Error,ErrorType.InvalidParameter);
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

        public TaskDto EditTask(int taskId, TaskDto task)
        {
            if (taskId == 0 || task==null)
                throw new NSIException("Parameter is invalid!", Level.Error, ErrorType.InvalidParameter);

            var taskTmp = _dbContext.Task.FirstOrDefault(x => x.TaskId == taskId);
            if (taskTmp != null)
            {
                taskTmp.Description = task.Description ?? taskTmp.Description;
                taskTmp.DueDate = task.DueDate != null ? task.DueDate : taskTmp.DueDate;
                taskTmp.Title = task.Title ?? taskTmp.Title;
                taskTmp.UserId = task.UserId != 0 ? task.UserId : taskTmp.UserId;
                _dbContext.SaveChanges();
                return Mappers.TaskRepository.MapToDto(taskTmp);
            }
            throw new NSIException("No data for parameter taskId=" + taskId.ToString(), Level.Info, ErrorType.MissingData);
        }

        public ICollection<TaskDto> GetTasksByUser(string user, bool userId = true)
        {
            if (user==null)
                throw new NSIException("User not valid",Level.Warning,ErrorType.InvalidParameter);
            ICollection<TaskDto> tasks =null;
            if (userId)
                tasks = SearchTasks(new TaskSearchCriteriaDto() { UserId = Convert.ToInt32(user) });
            else
            {
                Repository.UsersRepository usersRepository = new Repository.UsersRepository(_dbContext);
                var userInfo= usersRepository.GetUserInfoByUsername(user);
                tasks = SearchTasks(new TaskSearchCriteriaDto() { UserId = userInfo.UserId });
            }
            if(tasks.Count==0)
                throw new NSIException(userId ? "No data for userId = " + userId.ToString() : "No data for username = "+user, Level.Info, ErrorType.MissingData);

            return tasks;
        }

        /// <summary>
        /// Get tasks for date range.
        /// </summary>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <param name="by">Posible values: "DateModified", "DueDate", default:"DateCreated"</param>
        /// <returns>Collection of TaskDto</returns>
        public ICollection<TaskDto> GetTasksWithDateRange(DateTime dateTimeStart, DateTime dateTimeEnd, string by)
        {
            DateTime dateStart = Convert.ToDateTime(dateTimeStart);
            DateTime dateEnd = Convert.ToDateTime(dateTimeEnd);
            IEnumerable<Task> tasks;
            switch (by)
            {
                case "DateModified":
                    tasks = _dbContext.Task.Where(x => dateStart <= x.DateModified && x.DateModified <= dateEnd);
                    break;
                case "DueDate":
                    tasks = _dbContext.Task.Where(x => dateStart <= x.DueDate && x.DueDate <= dateEnd);
                    break;
                default:
                    tasks = _dbContext.Task.Where(x => dateStart <= x.DateCreated && x.DateCreated <= dateEnd);
                    break;
            };
            if (tasks != null)
            {
                ICollection<TaskDto> tasksDto = new List<TaskDto>();
                foreach (var item in tasks)
                {
                    tasksDto.Add(Mappers.TaskRepository.MapToDto(item));
                }
                return tasksDto;
            }
            throw new NSIException("No data in database!", Level.Info, ErrorType.MissingData);
        }
    }
}
