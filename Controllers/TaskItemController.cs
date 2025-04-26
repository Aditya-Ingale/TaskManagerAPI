using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;  // Ensure we have TaskStatus available here
using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetTaskItems()
        {
            var taskItems = await _context.TaskItems
                                          .Include(t => t.User)  // Include the User details
                                          .ToListAsync();

            var taskItemDTOs = taskItems.Select(task => MapToDTO(task)).ToList();
            return Ok(taskItemDTOs);
        }

        // GET: api/TaskItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDTO>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems
                                         .Include(t => t.User)  // Include the User details
                                         .FirstOrDefaultAsync(t => t.Id == id);

            if (taskItem == null)
            {
                return NotFound();
            }

            var taskItemDTO = MapToDTO(taskItem);
            return Ok(taskItemDTO);
        }

        // POST: api/TaskItem
        [HttpPost]
        public async Task<ActionResult<TaskItemDTO>> PostTaskItem(CreateTaskItemDTO createTaskItemDTO)
        {
            // Step 1: Validate if User exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == createTaskItemDTO.UserId);
            if (!userExists)        
                {
                    return BadRequest($"User with ID {createTaskItemDTO.UserId} does not exist.");
                }
            
            // Validate Task Data
            if (string.IsNullOrWhiteSpace(createTaskItemDTO.Title))
                {
                    return BadRequest("Task Title cannot be empty.");
                }
                
            if (string.IsNullOrWhiteSpace(createTaskItemDTO.Description))
                {
                    return BadRequest("Task Description cannot be empty.");
                }
                
            if (createTaskItemDTO.DueDate < DateTime.UtcNow)
                {
                    return BadRequest("Due Date cannot be in the past.");
                }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                    

            // Step 2: Parse Status to TaskStatus enum
            var taskStatus = Enum.TryParse<TaskManagerAPI.Models.TaskStatus>(createTaskItemDTO.Status, out var status)
                ? status
                : TaskManagerAPI.Models.TaskStatus.Pending;

            // Step 3: Create TaskItem
            var taskItem = new TaskItem
            {
                Title = createTaskItemDTO.Title,
                Description = createTaskItemDTO.Description,
                Status = taskStatus,
                DueDate = DateTime.SpecifyKind(createTaskItemDTO.DueDate, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = createTaskItemDTO.UserId
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            // Step 4: Reload the TaskItem with User info
            var savedTaskItem = await _context.TaskItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == taskItem.Id);

            var taskItemDTO = MapToDTO(savedTaskItem!);

            return CreatedAtAction(nameof(GetTaskItem), new { id = savedTaskItem!.Id }, taskItemDTO);
        }

        // PUT: api/TaskItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDTO>> UpdateTaskItem(int id, UpdateTaskItemDTO updateTaskItemDTO)
        {
            var taskItem = await _context.TaskItems.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            if (taskItem == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // Update fields
            taskItem.Title = updateTaskItemDTO.Title;
            taskItem.Description = updateTaskItemDTO.Description;
            taskItem.Status = Enum.TryParse<TaskManagerAPI.Models.TaskStatus>(updateTaskItemDTO.Status, out var parsedStatus)
                                ? parsedStatus
                                : TaskManagerAPI.Models.TaskStatus.Pending;
            taskItem.DueDate = DateTime.SpecifyKind(updateTaskItemDTO.DueDate, DateTimeKind.Utc);
            taskItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedDTO = MapToDTO(taskItem);
            return Ok(updatedDTO);
        }


        // DELETE: api/TaskItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Helper method to map TaskItem to TaskItemDTO
        private TaskItemDTO MapToDTO(TaskItem taskItem)
        {
            return new TaskItemDTO
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status.ToString(),  // Convert TaskStatus enum to string
                DueDate = taskItem.DueDate,
                CreatedAt = taskItem.CreatedAt,
                UpdatedAt = taskItem.UpdatedAt,
                UserId = taskItem.UserId,
                User = taskItem.User != null ? new UserDTO
                {
                    Id = taskItem.User.Id,
                    Name = taskItem.User.Name,
                    Email = taskItem.User.Email
                } : null  // Check for null before accessing properties
            };
        }
    }
}
