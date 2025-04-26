using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.DTO
{
    public class CreateTaskItemDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = "pending";

        [Required(ErrorMessage = "DueDate is required.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
