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
        ICollection<TaskDto> GetTasks(int? pageNumber=null, int? pageSize=null);
        bool DeleteTaskById(int taskId);
        bool EditTask(int taskId, TaskDto task);
        ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria, int pageNumber, int pageSize);
    }
}
