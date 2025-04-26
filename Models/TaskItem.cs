namespace TaskManagerAPI.Models
{
    public enum TaskStatus  
    {
        Pending,
        InProgress,
        Completed
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Use TaskStatus enum with default value set to Pending
        public TaskStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        // Automatically set CreatedAt and UpdatedAt to current time
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key for User
        public int UserId { get; set; }

        // Navigation property for User
        public User? User { get; set; }
    }
}
