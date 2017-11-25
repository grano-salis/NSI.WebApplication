using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface ITaskRepository
    {
        TaskDto CreateTask(TaskDto taskDto);
        ICollection<TaskDto> SearchTasks(TaskDto searchCriteria);
        TaskDto GetTaskById(int taskId);
        ICollection<TaskDto> GetTasks();
        bool DeleteTaskById(int taskId);
        bool EditTask(int taskId, TaskDto task);
    }
}
