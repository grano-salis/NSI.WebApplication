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
        ICollection<TaskDto> GetTasks(int? pageNumber=0, int? pageSize=20);
        bool DeleteTaskById(int taskId);
        TaskDto EditTask(int taskId, TaskDto task);
        ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria, int pageNumber, int pageSize);
    }
}
