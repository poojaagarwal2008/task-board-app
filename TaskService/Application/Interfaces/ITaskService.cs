
namespace TaskService.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(string name, string description, DateTime deadline);
        Task UpdateTaskAsync(Guid id, string name, string description, DateTime deadline, string column, bool isFavorite);
        Task DeleteTaskAsync(Guid id);
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(string? column = null);
        Task AttachFileToTaskAsync(Guid taskId, string? fileUrl);
    }
}
