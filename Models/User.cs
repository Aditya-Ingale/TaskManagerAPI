using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerAPI.Models
{
    [Table("users")] // Maps this class to the "users" table
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public string Password {get; set; } = string.Empty;
        
        public string PasswordHash { get; set; } = string.Empty;
        
        public string PasswordSalt { get; set; } = string.Empty;
    }
}
