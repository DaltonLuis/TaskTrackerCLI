using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

public class TaskManager
{
    private List<Task> tasks = new List<Task>();
    private int nextId = 1;
    private readonly string dataFile = "tasks.json"; // JSON file in project directory

    public TaskManager()
    {
        LoadTasks();
    }

    public void Add(string description)
    {
        var task = new Task(nextId++, description);
        tasks.Add(task);
        Console.WriteLine($"Task added successfully (ID: {task.Id}).");
        SaveTasks();
    }

    public void Update(int id, string newDescription)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.Description = newDescription;
        task.UpdatedAt = DateTime.UtcNow;
        Console.WriteLine("Task updated.");
        SaveTasks();
    }

    public void ListAllTasks()
    {
        if (!tasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var t in tasks)
        {
            Console.WriteLine(
                $"ID: {t.Id}, Desc: {t.Description}, Status: {t.Status}, " +
                $"Created: {t.CreatedAt}, Updated: {t.UpdatedAt}"
            );
        }
    }

    public void Delete(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        tasks.Remove(task);
        Console.WriteLine("Task deleted.");
        SaveTasks();
    }

    public void MarkInProgress(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.Status = "in-progress";
        task.UpdatedAt = DateTime.UtcNow;
        Console.WriteLine("Task marked as in-progress.");
        SaveTasks();
    }

    public void MarkDone(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.Status = "done";
        task.UpdatedAt = DateTime.UtcNow;
        Console.WriteLine("Task marked as done.");
        SaveTasks();
    }

    public void ListByStatus(string status)
    {
        var filteredTasks = tasks.Where(t => t.Status?.Trim().ToLower() == status.Trim().ToLower()).ToList();
        if (!filteredTasks.Any())
        {
            Console.WriteLine($"No tasks with status '{status}' found.");
            return;
        }

        foreach (var t in filteredTasks)
        {
            Console.WriteLine(
                $"ID: {t.Id}, Desc: {t.Description}, Status: {t.Status}, " +
                $"Created: {t.CreatedAt}, Updated: {t.UpdatedAt}"
            );
        }
    }

     // ------------------ JSON Persistence Methods ------------------

    private void SaveTasks()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(dataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks: {ex.Message}");
        }
    }

    private void LoadTasks()
    {
        try
        {
            if (!File.Exists(dataFile)) return;

            string json = File.ReadAllText(dataFile);
            tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();

            // Set nextId to max ID + 1 to avoid duplicates
            if (tasks.Any())
                nextId = tasks.Max(t => t.Id) + 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks: {ex.Message}");
        }
    }
}