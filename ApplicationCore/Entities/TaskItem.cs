using System;
using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class TaskItem : IEntity, ICreatedAndUpdated
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}