using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.TaskRepository;
using NSI.Repository.Interfaces;
using NSI.Repository;

namespace NSI.BLL
{
    public class TaskManipulation : ITaskManipulation
    {
        TaskRepository _taskRepository;

        public TaskManipulation()
        {
            _taskRepository = new TaskRepository(new IkarusEntities.IkarusContext());
        }

        public TaskManipulation(TaskRepository taskRepository)
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

        public TaskDto GetTaksById(int taskId)
        {
            return _taskRepository.GetTaskById(taskId);
        }

        public ICollection<TaskDto> GetTasks()
        {
            return _taskRepository.GetTasks();
        }
    }
}
