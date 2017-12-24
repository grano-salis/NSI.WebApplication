using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface ITaskManipulation
    {
        TaskDto GetTaksById(int taskId);
        TaskDto CreateTask(TaskDto taskDto);
        ICollection<TaskDto> GetTasks(int? pageNumber, int? pageSize);
        bool DeleteTaskById(int taskId);
        TaskDto EditTask(int taskId, TaskDto task);
        ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria, int? pageNumber, int? pageSize);
        ICollection<TaskDto> GetTasksByUserId(int userId, int? pageNumber, int? pageSize);
        ICollection<TaskDto> GetTasksByUsername(string username, int? pageNumber, int? pageSize);
        ICollection<TaskDto> GetTasksWithDueDateRange(DateTime dateTimeStart, DateTime dateTimeEnd, int? pageNumber, int? pageSize);
    }
}
