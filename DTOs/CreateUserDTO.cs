using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.DTO
{
    public class CreateUserDTO
    {
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string Email { get; set; } = string.Empty;
    }
}
