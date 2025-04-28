namespace TaskService.Application.Commands
{
    public record CreateTaskCommand(string Name, string Description, DateTime Deadline);
}
