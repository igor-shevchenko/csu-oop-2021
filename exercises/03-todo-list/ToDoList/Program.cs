using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList
{
    class TodoList
    {
        private List<Tuple<string, DateTime>> completeTasks;
        private List<Tuple<string, DateTime>> incompleteTasks;

        public TodoList()
        {
            completeTasks = new List<Tuple<string, DateTime>>();
            incompleteTasks = new List<Tuple<string, DateTime>>();
        }

        public void AddTask(string description, DateTime deadline)
        {
            incompleteTasks.Add(Tuple.Create(description, deadline));
        }

        public List<Tuple<string, DateTime>> GetOverdueTasks(DateTime currentDate)
        {
            return incompleteTasks
                .Where(task => task.Item2 <= currentDate)
                .ToList();
        }

        public void CompleteTask(int number)
        {
            var index = number - 1;
            if (index >= incompleteTasks.Count)
                throw new IndexOutOfRangeException("Неверный номер задачи");

            completeTasks.Add(incompleteTasks[index]);
            incompleteTasks.RemoveAt(index);
        }

        public void SaveToFile(string filename)
        {
            using (var sw = new StreamWriter(filename))
            {
                completeTasks.ForEach(
                    task => sw.WriteLine("{0}\t{1}\tcomplete", task.Item1, task.Item2)
                );
                incompleteTasks.ForEach(
                    task => sw.WriteLine("{0}\t{1}\tincomplete", task.Item1, task.Item2)
                );
            }
        }

        public static TodoList ReadFromFileOrCreate(string filename)
        {
            var todoList = new TodoList();
            try
            {
                using (var sr = new StreamReader(filename))
                {
                    while (true)
                    {
                        var line = sr.ReadLine();
                        if (line == null)
                            break;

                        var parts = line.Split('\t');
                        if (parts.Length != 3)
                            throw new FormatException("Неверный формат файла");

                        var text = parts[0];
                        var deadline = DateTime.Parse(parts[1]);
                        var status = parts[2];

                        todoList.AddTask(text, deadline);

                        if (status == "complete")
                        {
                            var allTasks = todoList.GetOverdueTasks(DateTime.MaxValue);
                            todoList.CompleteTask(allTasks.Count);
                        }
                    }
                }
            } catch(FileNotFoundException) { }

            return todoList;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Список задач:");
                var index = 1;

                completeTasks.ForEach(
                    task => Console.WriteLine("{0}. [x] {1}", index++, task.Item1)
                );
                incompleteTasks.ForEach(
                    task => Console.WriteLine("{0}. [ ] {1} (до {2})", index++, task.Item1, task.Item2)
                );

                var command = ReadFromConsole();

                if (command == "save")
                {
                    SaveToFile("tasks.txt");
                }
                else if (command == "exit")
                {
                    return;
                }
                else if (command == "complete")
                {
                    var numberString = ReadFromConsole();
                    if (!int.TryParse(numberString, out var number))
                    {
                        continue;
                    }
                    CompleteTask(number - completeTasks.Count);
                } else if (command == "add")
                {
                    var description = ReadFromConsole();
                    var deadlineString = ReadFromConsole();
                    if (!DateTime.TryParse(deadlineString, out var deadline))
                    {
                        continue;
                    }
                    incompleteTasks.Add(Tuple.Create(description, deadline));
                } else if (command == "clean")
                {
                    completeTasks = new List<Tuple<string, DateTime>>();
                }
            }

        }

        private string ReadFromConsole()
        {
            string result;
            do
            {
                result = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(result));

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var todoList = TodoList.ReadFromFileOrCreate("tasks.txt");
            todoList.AddTask("Съесть мягких французских булок", DateTime.Today);
            todoList.AddTask("Выпить чаю", DateTime.Today + TimeSpan.FromDays(1));
            todoList.Start();
        }
    }
}
