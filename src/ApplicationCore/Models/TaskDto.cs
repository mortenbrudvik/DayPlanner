namespace ApplicationCore.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public StatusDto Status { get; set; }
        public string StatusText => Status.ToString();
    }

    public enum StatusDto
    {
        Todo = 1,
        InProgress = 2,
        Done = 3
    }
}