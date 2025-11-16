
namespace TaskTruckerCLI{

    public class Program
    {
        public static void Main(){
            var taskManager = new TaskManager();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to TaskTrucker CLI!");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine();
            Console.WriteLine("add - Add tasks");
            Console.WriteLine("update - Update tasks");
            Console.WriteLine("delete - Delete tasks");
            Console.WriteLine("mark-in-progress - Mark a task as in progress");
            Console.WriteLine("mark-done - Mark a task as done");
            Console.WriteLine("list (optional: 'todo', 'in-progress', 'done') - List tasks by status");
            Console.WriteLine("exit - Exiting the application");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine();
            do
            {
                Console.Write("task-cli: ");
                string switch_on = Console.ReadLine() ?? "";

                // Separa comando e argumento
                var parts = ParseCommand(switch_on);

                string command = parts.command;
                string argument = parts.argument ?? "";
                string? argument2 = parts.argument2;

                switch (command)
                {
                    case "add":
                        string description = argument ?? "";
                        taskManager.Add(description.Trim('"'));
                        break;

                    case "update":
                        int updateId = int.Parse(argument ?? "0");
                        string updateDescription = argument2 ?? "";
                        taskManager.Update(updateId, updateDescription.Trim('"'));
                        break;

                    case "delete":
                        int deleteId = int.Parse(argument ?? "0");
                        taskManager.Delete(deleteId);
                        break;

                    case "mark-in-progress":
                        int inProgressId = int.Parse(argument ?? "0");
                        taskManager.MarkInProgress(inProgressId);
                        break;

                    case "mark-done":
                        int doneId = int.Parse(argument ?? "0");
                        taskManager.MarkDone(doneId);
                        break;

                    case "list":
                        if (string.IsNullOrWhiteSpace(argument))
                            taskManager.ListAllTasks();
                        else
                            taskManager.ListByStatus(argument);
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            } while (true);
        }
        private static (string command, string? argument, string? argument2) ParseCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ("", null, null);

            input = input.Trim();

            // Quebra o comando da parte restante da linha
            int firstSpace = input.IndexOf(' ');
            if (firstSpace == -1)
                return (input, null, null);

            string command = input.Substring(0, firstSpace);
            string rest = input.Substring(firstSpace + 1).Trim();
            
            // Se argumento está entre aspas
            if (rest.StartsWith("\""))
            {
                // Encontra fechamento das aspas
                int closingQuote = rest.IndexOf('"', 1);
                if (closingQuote != -1)
                {
                    string arg1 = rest.Substring(1, closingQuote - 1);
                    string arg2 = rest.Length > closingQuote + 1 
                        ? rest.Substring(closingQuote + 1).Trim() 
                        : string.Empty;

                    return (command, arg1, arg2);
                }
            }

            // Se não tem aspas → quebra por espaço
            var parts = rest.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return (command, parts[0], null);

            return (command, parts[0], parts[1]);
        }
    } 

    
}
