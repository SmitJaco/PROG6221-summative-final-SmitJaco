//tasks and reminders
using System.ComponentModel;

namespace ChatBotFinalPoe.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = "";
        private string _description = "";
        private DateTime? _reminder;
        private bool _isCompleted;
        public bool IsCompleted
        {//constructor for taks and reminders
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(nameof(IsCompleted)); }
        }
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public DateTime? Reminder
        {
            get => _reminder;
            set { _reminder = value; OnPropertyChanged(nameof(Reminder)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}