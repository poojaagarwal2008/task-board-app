using TaskService.Infrastructure.Services;
using Xunit;

namespace TaskBoard.Tests
{
    public class TaskServiceTests
    {
        private readonly TaskServices _taskService;

        public TaskServiceTests()
        {
            _taskService = new TaskServices();
        }

        [Fact]
        public async Task CreateTask_ShouldAddTask()
        {
            var task = await _taskService.CreateTaskAsync("Test", "Desc", DateTime.UtcNow);
            Assert.NotNull(task);
            Assert.That(task.Name, Is.EqualTo("Test")); // Fix: Use Assert.Equal from Xunit
            Assert.That(task.Description, Is.EqualTo("Desc")); // Fix: Use Assert.Equal from Xunit
            Assert.That(task.Column, Is.EqualTo("ToDo")); // Fix: Use Assert.Equal from Xunit
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateSuccessfully()
        {
            // Arrange
            var task = await _taskService.CreateTaskAsync("Old Name", "Old Desc", DateTime.UtcNow);

            // Act
            await _taskService.UpdateTaskAsync(task.Id, "New Name", "New Desc", task.Deadline.AddDays(1), "InProgress", true);
            var updatedTask = await _taskService.GetTaskByIdAsync(task.Id);

            // Assert
            Assert.NotNull(updatedTask);
            Assert.That(task.Name, Is.EqualTo("Test")); // Fix: Use Assert.Equal from Xunit
            Assert.That(task.Description, Is.EqualTo("Desc")); // Fix: Use Assert.Equal from Xunit
            Assert.That(task.Column, Is.EqualTo("InProgress")); // Fix: Use Assert.Equal from Xunit
            Assert.True(updatedTask.IsFavorite); // Fix: Use Assert.True from Xunit
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTask()
        {
            // Arrange
            var task = await _taskService.CreateTaskAsync("Task To Delete", "Desc", DateTime.UtcNow);

            // Act
            await _taskService.DeleteTaskAsync(task.Id);
            var deletedTask = await _taskService.GetTaskByIdAsync(task.Id);

            // Assert
            Assert.Null(deletedTask);
        }

        [Fact]
        public async Task GetTaskById_ShouldReturnCorrectTask()
        {
            // Arrange
            var task = await _taskService.CreateTaskAsync("Specific Task", "Desc", DateTime.UtcNow);

            // Act
            var retrievedTask = await _taskService.GetTaskByIdAsync(task.Id);

            // Assert
            Assert.NotNull(retrievedTask);
            Assert.That(task.Id, Is.EqualTo(retrievedTask.Id));
        }

        [Fact]
        public async Task GetAllTasks_FilterByColumn_ShouldWork()
        {
            // Arrange
            await _taskService.CreateTaskAsync("ToDo Task", "Desc", DateTime.UtcNow);
            var inProgressTask = await _taskService.CreateTaskAsync("Progress Task", "Desc", DateTime.UtcNow);
            await _taskService.UpdateTaskAsync(inProgressTask.Id, inProgressTask.Name, inProgressTask.Description, inProgressTask.Deadline, "InProgress", false);

            // Act
            var todoTasks = await _taskService.GetAllTasksAsync("ToDo");
            var progressTasks = await _taskService.GetAllTasksAsync("InProgress");

            foreach (var t in todoTasks) // Fix: Replace Assert.All with a foreach loop
            {
                Assert.That(t.Column, Is.EqualTo("ToDo"));
            }

            foreach (var t in progressTasks) // Fix: Replace Assert.All with a foreach loop
            {
                Assert.That(t.Column, Is.EqualTo("InProgress"));
            }

        }
    }
}
