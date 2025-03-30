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

        public virtual void EditTask(string description, int priority)
        {
            Description = description;
            Priority = priority;
        }
    }

    public class WorkTask : Task
    {
        public DateTime DueDate { get; set; }

        public WorkTask(string description, int priority, DateTime dueDate)
            : base(description, priority)
        {
            DueDate = dueDate;
        }

        public override void CompleteTask()
        {
            base.CompleteTask();
            Console.WriteLine("You completed a work task. Nice job! 💼");
        }

        public override void EditTask(string description, int priority)
        {
            base.EditTask(description, priority);

            Console.WriteLine("Enter new due day (1-31):");
            string? dayInput = Console.ReadLine();
            Console.WriteLine("Enter new due month (1-12):");
            string? monthInput = Console.ReadLine();
            Console.WriteLine("Enter new due year (e.g. 2025):");
            string? yearInput = Console.ReadLine();

            if (int.TryParse(dayInput, out int day) &&
                int.TryParse(monthInput, out int month) &&
                int.TryParse(yearInput, out int year) &&
                DateTime.TryParse($"{year}-{month}-{day}", out DateTime newDueDate))
            {
                DueDate = newDueDate;
            }
            else
            {
                Console.WriteLine("\u2757 Invalid due date input. Keeping the old due date.");
            }
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

        public void CompleteTask(bool isWorkTask)
        {
            var filteredTasks = tasks.Where(t => t is WorkTask == isWorkTask).OrderBy(t => t.Priority).ToList();

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < filteredTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredTasks[i].Description} - Priority: {filteredTasks[i].Priority}");
            }

            Console.WriteLine("Enter task number to complete:");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int taskIndex) && taskIndex >= 1 && taskIndex <= filteredTasks.Count)
            {
                var selectedTask = filteredTasks[taskIndex - 1];
                selectedTask.CompleteTask();
                tasks.Remove(selectedTask);
            }
            else
            {
                Console.WriteLine("\u2757 Invalid task number.");
            }
        }

        public void EditTask(bool isWorkTask)
        {
            var filteredTasks = tasks.Where(t => t is WorkTask == isWorkTask).OrderBy(t => t.Priority).ToList();

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < filteredTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredTasks[i].Description} - Priority: {filteredTasks[i].Priority}");
            }

            Console.WriteLine("Enter task number to edit:");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int taskIndex) && taskIndex >= 1 && taskIndex <= filteredTasks.Count)
            {
                var selectedTask = filteredTasks[taskIndex - 1];

                Console.WriteLine("Enter new description:");
                string? newDesc = Console.ReadLine();
                Console.WriteLine("Enter new priority:");
                string? newPriorityInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newDesc) && int.TryParse(newPriorityInput, out int newPriority))
                {
                    selectedTask.EditTask(newDesc, newPriority);
                    Console.WriteLine("Task updated successfully!");
                }
                else
                {
                    Console.WriteLine("\u2757 Invalid input. Task not updated.");
                }
            }
            else
            {
                Console.WriteLine("\u2757 Invalid task number.");
            }
        }

        public void DisplayTasks(bool isWorkTask)
        {
            var filteredTasks = tasks.Where(t => t is WorkTask == isWorkTask).OrderBy(t => t.Priority).ToList();
            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            for (int i = 0; i < filteredTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredTasks[i].Description} - Priority: {filteredTasks[i].Priority} - Type: {filteredTasks[i].GetType().Name}");

                if (filteredTasks[i] is WorkTask workTask)
                {
                    Console.WriteLine($"   🗓️ Due Date: {workTask.DueDate:dd-MM-yyyy}");
                }
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
                Console.WriteLine("To-Do List Menu:");
                Console.WriteLine("1. Add Work Task");
                Console.WriteLine("2. Add Personal Task");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. Edit Task");
                Console.WriteLine("5. Display Tasks");
                Console.WriteLine("6. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter work task description:");
                        string? workDesc = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(workDesc))
                        {
                            Console.WriteLine("\u2757 Description cannot be empty.");
                            break;
                        }

                        Console.WriteLine("Enter priority:");
                        string? workPriorityInput = Console.ReadLine();

                        if (!int.TryParse(workPriorityInput, out int workPriority))
                        {
                            Console.WriteLine("\u2757 Invalid priority.");
                            break;
                        }

                        Console.WriteLine("Enter due day (1-31):");
                        string? dayInput = Console.ReadLine();
                        Console.WriteLine("Enter due month (1-12):");
                        string? monthInput = Console.ReadLine();
                        Console.WriteLine("Enter due year (e.g. 2025):");
                        string? yearInput = Console.ReadLine();

                        if (!int.TryParse(dayInput, out int day) ||
                            !int.TryParse(monthInput, out int month) ||
                            !int.TryParse(yearInput, out int year) ||
                            !DateTime.TryParse($"{year}-{month}-{day}", out DateTime dueDate))
                        {
                            Console.WriteLine("\u2757 Invalid date.");
                            break;
                        }

                        toDoList.AddTask(new WorkTask(workDesc, workPriority, dueDate));
                        Console.Clear();
                        break;

                    case "2":
                        Console.WriteLine("Enter personal task description:");
                        string? personalDesc = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(personalDesc))
                        {
                            Console.WriteLine("\u2757 Description cannot be empty.");
                            break;
                        }

                        Console.WriteLine("Enter priority:");
                        string? personalPriorityInput = Console.ReadLine();

                        if (!int.TryParse(personalPriorityInput, out int personalPriority))
                        {
                            Console.WriteLine("\u2757 Invalid priority.");
                            break;
                        }

                        toDoList.AddTask(new PersonalTask(personalDesc, personalPriority));
                        Console.Clear();
                        break;

                    case "3":
                        Console.WriteLine("Select task type to complete:");
                        Console.WriteLine("1. Work Task");
                        Console.WriteLine("2. Personal Task");
                        string? completeType = Console.ReadLine();
                        if (completeType == "1")
                            toDoList.CompleteTask(true);
                        else if (completeType == "2")
                            toDoList.CompleteTask(false);
                        else
                            Console.WriteLine("\u2757 Invalid option.");

                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "4":
                        Console.WriteLine("Select task type to edit:");
                        Console.WriteLine("1. Work Task");
                        Console.WriteLine("2. Personal Task");
                        string? editType = Console.ReadLine();
                        if (editType == "1")
                            toDoList.EditTask(true);
                        else if (editType == "2")
                            toDoList.EditTask(false);
                        else
                            Console.WriteLine("\u2757 Invalid option.");

                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "5":
                        Console.WriteLine("Select task type to display:");
                        Console.WriteLine("1. Work Task");
                        Console.WriteLine("2. Personal Task");
                        string? displayType = Console.ReadLine();
                        if (displayType == "1")
                            toDoList.DisplayTasks(true);
                        else if (displayType == "2")
                            toDoList.DisplayTasks(false);
                        else
                            Console.WriteLine("\u2757 Invalid option.");

                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("\u2757 Invalid option.");
                        break;
                }
            }
        }
    }
}