using Ardalis.SmartEnum;

namespace ApplicationCore.Entities
{
    public abstract class TaskStatus : SmartEnum<TaskStatus>
    {
        public static readonly TaskStatus Todo = new TodoStatus();
        public static readonly TaskStatus InProgress = new InProgressStatus();
        public static readonly TaskStatus Done = new DoneStatus();

        protected TaskStatus(string name, int value) : base(name, value) { }

        private sealed class TodoStatus : TaskStatus
        {
            public TodoStatus() : base("Todo", 1) { }
        }

        private sealed class InProgressStatus : TaskStatus
        {
            public InProgressStatus() : base("In Progress", 2) { }
        }

        private sealed class DoneStatus : TaskStatus
        {
            public DoneStatus() : base("Done", 3) { }
        }
    }
}