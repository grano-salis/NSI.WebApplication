using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface ITaskRepository
    {
        TaskDto CreateTask(TaskDto taskDto);
        ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria);
        TaskDto GetTaskById(int taskId);
        ICollection<TaskDto> GetTasks();
        bool DeleteTaskById(int taskId);
        TaskDto EditTask(int taskId, TaskDto task);
        ICollection<TaskDto> GetTasksByUser(string user, bool userId = true);
        ICollection<TaskDto> GetTasksWithDateRange(DateTime dateTimeStart, DateTime dateTimeEnd, string by);
    }
}
