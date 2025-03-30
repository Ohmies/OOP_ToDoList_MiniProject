using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoListOOP
{
    public abstract class Task
    {
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int Priority { get; set; }

        public Task(string description, int priority)
        {
            Description = description;
            IsComplete = false;
            Priority = priority;
        }

        public virtual void CompleteTask()
        {
            IsComplete = true;
            Console.WriteLine("Task completed!");
        }
    }

    public class WorkTask : Task
    {
        public WorkTask(string description, int priority) : base(description, priority) { }

        public override void CompleteTask()
        {
            base.CompleteTask();
            Console.WriteLine("You completed a work task. Nice job! 💼");
        }
    }

    public class PersonalTask : Task
    {
        public PersonalTask(string description, int priority) : base(description, priority) { }

        public override void CompleteTask()
        {
            base.CompleteTask();
            Console.WriteLine("Personal task done. Time to relax~ 🌴");
        }
    }

    public class ToDoList
    {
        private List<Task> tasks;

        public ToDoList()
        {
            tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void CompleteTask(int taskNumber)
        {
            if (taskNumber >= 0 && taskNumber < tasks.Count)
            {
                tasks[taskNumber].CompleteTask();
            }
        }

        public void RemoveTask(int taskNumber)
        {
            if (taskNumber >= 0 && taskNumber < tasks.Count)
            {
                tasks.RemoveAt(taskNumber);
            }
        }

        public void DisplayTasks()
        {
            var sortedTasks = tasks.OrderBy(t => t.Priority).ToList();
            for (int i = 0; i < sortedTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedTasks[i].Description} - {(sortedTasks[i].IsComplete ? "✅ Complete" : "❌ Incomplete")} - Priority: {sortedTasks[i].Priority} - Type: {sortedTasks[i].GetType().Name}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var toDoList = new ToDoList();

            while (true)
            {
                Console.WriteLine("📋 To-Do List OOP Edition");
                Console.WriteLine("1. Add Work Task");
                Console.WriteLine("2. Add Personal Task");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. Display Tasks");
                Console.WriteLine("5. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter work task description:");
                        string? workDesc = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(workDesc))
                        {
                            Console.WriteLine("❗ Description cannot be empty.");
                            break;
                        }

                        Console.WriteLine("Enter priority:");
                        string? workPriorityInput = Console.ReadLine();

                        if (!int.TryParse(workPriorityInput, out int workPriority))
                        {
                            Console.WriteLine("❗ Invalid priority.");
                            break;
                        }

                        toDoList.AddTask(new WorkTask(workDesc, workPriority));
                        Console.Clear();
                        break;

                    case "2":
                        Console.WriteLine("Enter personal task description:");
                        string? personalDesc = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(personalDesc))
                        {
                            Console.WriteLine("❗ Description cannot be empty.");
                            break;
                        }

                        Console.WriteLine("Enter priority:");
                        string? personalPriorityInput = Console.ReadLine();

                        if (!int.TryParse(personalPriorityInput, out int personalPriority))
                        {
                            Console.WriteLine("❗ Invalid priority.");
                            break;
                        }

                        toDoList.AddTask(new PersonalTask(personalDesc, personalPriority));
                        Console.Clear();
                        break;

                    case "3":
                        toDoList.DisplayTasks();
                        Console.WriteLine("Enter task number to complete:");
                        string? taskNumInput = Console.ReadLine();

                        if (!int.TryParse(taskNumInput, out int taskNumber))
                        {
                            Console.WriteLine("❗ Invalid task number.");
                            break;
                        }

                        toDoList.CompleteTask(taskNumber - 1);
                        toDoList.RemoveTask(taskNumber - 1);
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "4":
                        toDoList.DisplayTasks();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("❗ Invalid option.");
                        break;
                }
            }
        }
    }
}
