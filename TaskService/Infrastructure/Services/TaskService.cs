using TaskService.Application.Interfaces;

namespace TaskService.Infrastructure.Services
{
    public class TaskServices : ITaskService
    {
        private readonly List<TaskItem> _tasks = new();

        public async Task<TaskItem> CreateTaskAsync(string name, string description, DateTime deadline)
        {
            var task = new TaskItem(name, description, deadline);
            _tasks.Add(task);
            return await Task.FromResult(task);
        }

        public async Task UpdateTaskAsync(Guid id, string name, string description, DateTime deadline, string column, bool isFavorite)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);
            if (task == null) throw new KeyNotFoundException();
            task.Update(name, description, deadline, column, isFavorite);
            await Task.CompletedTask;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);
            if (task != null) _tasks.Remove(task);
            await Task.CompletedTask;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            return await Task.FromResult(_tasks.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(string? column = null)
        {
            var query = _tasks.AsQueryable();

            if (!string.IsNullOrEmpty(column))
                query = query.Where(x => x.Column == column);

            // Sorting: Favorited first, then alphabetically by name
            var sortedTasks = query
                .OrderByDescending(x => x.IsFavorite)  // Favorited tasks first
                .ThenBy(x => x.Name)                   // Then alphabetical by name
                .ToList();

            return await Task.FromResult(sortedTasks);
        }


        public async Task AttachFileToTaskAsync(Guid taskId, string? fileUrl)
        {
            if (fileUrl == null) throw new ArgumentNullException(nameof(fileUrl));

            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) throw new KeyNotFoundException();

            task.AddFileUrl(fileUrl);

            await Task.CompletedTask;
        }

    }
}
