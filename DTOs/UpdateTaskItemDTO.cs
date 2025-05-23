namespace TaskManagerAPI.DTO
{
    public class UpdateTaskItemDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; 
        public DateTime DueDate { get; set; }
    }
}
