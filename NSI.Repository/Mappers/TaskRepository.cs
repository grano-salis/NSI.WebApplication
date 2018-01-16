using IkarusEntities;
using NSI.DC.TaskRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public static partial class TaskRepository
    {
        public static Task MapToDbEntity(TaskDto taskDto)
        {
            return new Task()
            {
                DateCreated=taskDto.DateCreated,
                DateModified=taskDto.DateModified,
                Description=taskDto.Description,
                DueDate=taskDto.DueDate,
                IsDeleted=taskDto.IsDeleted,
                TaskId=taskDto.TaskId,
                Title=taskDto.Title,
                UserId=taskDto.UserId,
                Status=taskDto.Status
            };
        }

        public static TaskDto MapToDto(Task task)
        {
            return new TaskDto()
            {
                DateCreated=task.DateCreated,
                DateModified=task.DateModified,
                Description=task.Description,
                DueDate=task.DueDate,
                IsDeleted=task.IsDeleted,
                TaskId=task.TaskId,
                Title=task.Title,
                UserId=task.UserId,
                Status=task.Status
            };
        }
    }
}
