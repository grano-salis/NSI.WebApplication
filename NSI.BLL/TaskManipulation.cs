using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.TaskRepository;
using NSI.Repository.Interfaces;
using NSI.Repository;
using NSI.BLL.Helpers;

namespace NSI.BLL
{
    public class TaskManipulation : ITaskManipulation
    {
        readonly ITaskRepository _taskRepository;


        public TaskManipulation(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TaskDto CreateTask(TaskDto taskDto)
        {
            return _taskRepository.CreateTask(taskDto);
        }

        public bool DeleteTaskById(int taskId)
        {
            return _taskRepository.DeleteTaskById(taskId);
        }

        public TaskDto EditTask(int taskId, TaskDto task)
        {
            return _taskRepository.EditTask(taskId, task);
        }

        public TaskDto GetTaksById(int taskId)
        {
            return _taskRepository.GetTaskById(taskId);
        }

        public ICollection<TaskDto> GetTasks(int? pageNumber, int? pageSize)
        {
            var tasks= _taskRepository.GetTasks();
            if (pageNumber != null && pageSize != null)
            {
                pageNumber = pageNumber ?? 1;
                pageSize = pageSize ?? 200;
                return PagingHelper<TaskDto>.PagedList(tasks, (int)pageNumber, (int)pageSize);
            }
            return tasks;
        }

        public ICollection<TaskDto> GetTasksByUserId(int userId, int? pageNumber, int? pageSize)
        {
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 200;
            return PagingHelper<TaskDto>.PagedList(_taskRepository.GetTasksByUser(userId.ToString()), (int)pageNumber, (int)pageSize);
        }

        public ICollection<TaskDto> GetTasksByUsername(string username, int? pageNumber, int? pageSize)
        {
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 200;
            return PagingHelper<TaskDto>.PagedList(_taskRepository.GetTasksByUser(username,false), (int)pageNumber, (int)pageSize);
        }

        public ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria, int? pageNumber, int? pageSize)
        {
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 200;
            return PagingHelper<TaskDto>.PagedList(_taskRepository.SearchTasks(searchCriteria), (int)pageNumber, (int)pageSize);
        }

        public ICollection<TaskDto> GetTasksWithDueDateRange(DateTime dateTimeStart, DateTime dateTimeEnd, int? pageNumber, int? pageSize)
        {
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 200;
            var by = "DueDate";
            return PagingHelper<TaskDto>.PagedList(_taskRepository.GetTasksWithDateRange(dateTimeStart, dateTimeEnd, by), (int)pageNumber, (int)pageSize);
        }

        public int GetTasksWithDueDateRangeCount(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            return _taskRepository.GetTasksWithDateRange(dateTimeStart, dateTimeEnd, "DueDate").Count;
        }
    }
}