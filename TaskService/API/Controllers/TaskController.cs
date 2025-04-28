using Microsoft.AspNetCore.Mvc;
using TaskService.Application.Interfaces;

namespace TaskService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Create a new task
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(string name, string description, DateTime deadline)
        {
            var task = await _taskService.CreateTaskAsync(name, description, deadline);
            return Ok(task);
        }

        // Get tasks by column
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(string? column)
        {
            var tasks = await _taskService.GetAllTasksAsync(column);
            return Ok(tasks);
        }

        // Attach a file to a task
        [HttpPost("{taskId}/attachfile")]
        public async Task<IActionResult> AttachFile(Guid taskId, [FromBody] AttachFileRequest request)
        {
            await _taskService.AttachFileToTaskAsync(taskId, request.FileUrl);
            return NoContent();
        }
    }

    // Request DTO
    public class AttachFileRequest
    {
        public string? FileUrl { get; set; }
    }
}
