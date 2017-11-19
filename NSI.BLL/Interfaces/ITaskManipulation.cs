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
        ICollection<TaskDto> GetTasks();
        bool DeleteTaskById(int taskId);
    }
}
