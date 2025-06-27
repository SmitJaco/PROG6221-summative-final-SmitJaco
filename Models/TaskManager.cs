using System;
using System.Collections.ObjectModel;
using System.Linq;
namespace ChatBotFinalPoe.Models
{
    //tasks and reminders
    public class TaskManager
    {
        
        public ObservableCollection<TaskItem> Tasks { get; } = new();

        // Creates and adds a new task with title, description, and reminder
        public void AddTask(string title, string description, string reminderText)
        {
            var task = new TaskItem
            {
                Title = title,
                
                Description = string.IsNullOrEmpty(description) ?
                    $"Ensure your {title.ToLower()} to protect your data." : description,
                
                Reminder = ParseReminder(reminderText)
            };
            Tasks.Add(task);
        }

        // Removes task at specified index
        public void DeleteTask(int index)
        {
            
            if (index >= 0 && index < Tasks.Count)
            {
                Tasks.RemoveAt(index);
            }
        }

        // Converts reminder text to DateTime
        public static DateTime? ParseReminder(string reminder)
        {
            if (string.IsNullOrEmpty(reminder)) return null;

            var parts = reminder.ToLower().Split(' ');
            // "X days" format 
            if (parts.Length >= 2 && int.TryParse(parts[0], out int days) && parts[1].StartsWith("day"))
                return DateTime.Now.AddDays(days);

            //direct date format
            if (DateTime.TryParse(reminder, out DateTime date))
                return date;

           
            return null;
        }
    }
}