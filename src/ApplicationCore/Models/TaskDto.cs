using ApplicationCore.Entities;

namespace ApplicationCore.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public TaskStatus Status { get; set; }
    }
}