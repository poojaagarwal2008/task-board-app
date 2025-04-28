namespace TaskService.Application.Commands
{
    public record UpdateTaskCommand(Guid Id, string Name, string Description, DateTime Deadline, string Column, bool IsFavorite);
}
