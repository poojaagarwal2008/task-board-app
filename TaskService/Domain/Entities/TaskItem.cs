public class TaskItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime Deadline { get; private set; }
    public string Column { get; private set; }
    public bool IsFavorite { get; private set; }
    public List<string> FileUrls { get; private set; } = new List<string>(); // <-- ADD THIS

    public TaskItem(string name, string description, DateTime deadline)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Deadline = deadline;
        Column = "ToDo"; // Default column
        IsFavorite = false;
    }

    public void Update(string name, string description, DateTime deadline, string column, bool isFavorite)
    {
        Name = name;
        Description = description;
        Deadline = deadline;
        Column = column;
        IsFavorite = isFavorite;
    }

    public void AddFileUrl(string url)
    {
        FileUrls.Add(url);
    }
}
