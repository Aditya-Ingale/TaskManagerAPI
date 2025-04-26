namespace TaskManagerAPI.DTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }  // Mark as required
        public required string Email { get; set; }  // Mark as required
    }
}
