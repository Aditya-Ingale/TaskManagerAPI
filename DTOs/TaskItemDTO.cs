using TaskManagerAPI.DTO; // Ensure this is added to import UserDTO

namespace TaskManagerAPI.DTO
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Set Status to the default "pending" string value if not provided
        public string Status { get; set; } = "pending"; 

        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }

        // User can be null, so we use nullable UserDTO type
        public UserDTO? User { get; set; }  
    }
}
