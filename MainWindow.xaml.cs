using ChatBotFinalPoe.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatBotFinalPoe
{
    public partial class MainWindow : Window
    {
        // Core service instances for application functionality
        private readonly ChatService _chatService = new();
        private readonly TaskManager _taskManager = new();
        private readonly QuizEngine _quizEngine = new();
        private readonly ActivityLogger _activityLogger = new();
        private ResponseHandler? _chatbot;

        // Initialize UI components and data bindings
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize UI bindings
            ChatHistory.ItemsSource = _chatService.ChatMessages;
            TaskList.ItemsSource = _taskManager.Tasks;
            ActivityLog.ItemsSource = _activityLogger.VisibleLog;
            QuizAnswer.ItemsSource = _quizEngine.CurrentQuestion?.Options ?? new List<string>();

            // Initial setup
            _chatService.DisplayWelcome();
            DeleteTaskButton.IsEnabled = false;
            SubmitAnswerButton.IsEnabled = false;
            QuizAnswer.IsEnabled = false;
        }

        #region Chat Handlers
        // Handle user input and send messages
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            _chatService.AddMessage($"You: {input}");

            if (_chatbot is null)
            {
                _chatbot = new ResponseHandler(input);
                _chatService.AddMessage($"Chatbot: Nice to meet you, {input}! How can I assist you today?");
                UserInput.Clear();
                return;
            }

            ProcessInput(input);
            UserInput.Clear();
        }

        // Process user input and generate appropriate responses
        private void ProcessInput(string input)
        {
            if (TryHandleCommands(input)) return;

            var response = _chatbot?.GetResponse(input.ToLower())
                ?? "I'm not sure how to answer that. Try asking about cybersecurity topics!";

            _chatService.AddMessage($"Chatbot: {response}");
            _activityLogger.Log($"Responded to: {input}");
        }

        // Parse and execute various chat commands
        private bool TryHandleCommands(string input)
        {
            var (isCommand, commandType, taskTitle, reminder, topic) = NlpProcessor.ProcessTaskCommand(input);

            if (isCommand)
            {
                switch (commandType)
                {
                    case NlpProcessor.CommandType.AddTask:
                        if (!string.IsNullOrEmpty(taskTitle))
                        {
                            _taskManager.AddTask(taskTitle.Trim(), "", reminder ?? "");
                            var task = _taskManager.Tasks[^1];
                            _chatService.AddMessage($"Chatbot: Added task: {task.Title}" +
                                (task.Reminder.HasValue ? $" (Reminder: {task.Reminder.Value:dd/MM/yyyy})" : ""));
                            _activityLogger.Log($"Task added: {task.Title}");
                        }
                        break;

                    case NlpProcessor.CommandType.AddReminder:
                        if (!string.IsNullOrEmpty(taskTitle) && !string.IsNullOrEmpty(reminder))
                        {
                            var existingTask = _taskManager.Tasks.FirstOrDefault(t => t.Title.Trim().Equals(taskTitle.Trim(), StringComparison.OrdinalIgnoreCase));
                            if (existingTask != null)
                            {
                                existingTask.Reminder = TaskManager.ParseReminder(reminder);
                                string reminderMessage = existingTask.Reminder.HasValue
                                    ? $"Chatbot: Reminder set for task '{taskTitle.Trim()}' to {existingTask.Reminder.Value:dd/MM/yyyy}"
                                    : $"Chatbot: Invalid reminder format for task '{taskTitle.Trim()}'. Please use 'in X days' or a valid date.";
                                _chatService.AddMessage(reminderMessage);
                                _activityLogger.Log($"Reminder added for task: {taskTitle.Trim()}");
                            }
                            else
                            {
                                _taskManager.AddTask(taskTitle.Trim(), "", reminder);
                                var newTask = _taskManager.Tasks[^1];
                                if (newTask.Reminder.HasValue)
                                {
                                    _chatService.AddMessage($"Chatbot: Task '{taskTitle.Trim()}' created with reminder set to {newTask.Reminder.Value:dd/MM/yyyy}");
                                    _activityLogger.Log($"New task '{taskTitle.Trim()}' created with reminder");
                                }
                                else
                                {
                                    _chatService.AddMessage($"Chatbot: Failed to create task '{taskTitle.Trim()}' with invalid reminder format. Please use 'in X days' or a valid date.");
                                }
                            }
                        }
                        else
                        {
                            _chatService.AddMessage("Chatbot: Please specify a task and reminder (e.g., 'add reminder check email in 2 days').");
                        }
                        break;

                    case NlpProcessor.CommandType.StartQuiz:
                        StartQuizFromChat();
                        break;

                    case NlpProcessor.CommandType.ListTasks:
                        ListTasksInChat();
                        break;

                    case NlpProcessor.CommandType.InfoRequest:
                        HandleInfoRequest(topic);
                        break;
                }

                return true;
            }

            return false;
        }

        // Provide cybersecurity information based on topic
        private void HandleInfoRequest(string? topic)
        {
            string response = topic switch
            {
                "passwords" => "Chatbot: A strong password should be at least 12 characters long, include numbers, symbols, and a mix of upper/lowercase letters. Avoid reusing passwords!",
                "phishing" => "Chatbot: Phishing emails often have spelling errors, suspicious links, or urgent requests. Always verify the sender and report suspicious emails!",
                "security" or "safety" => "Chatbot: To stay secure, enable two-factor authentication, keep software updated, and avoid public Wi-Fi without a VPN.",
                "more" or "about" or "tips" or "information" or "details" or "general" or null => "Chatbot: Ask me about 'passwords', 'phishing', or 'security' for specific tips!",
                _ => "Chatbot: I don't have info on that topic yet. Try 'passwords', 'phishing', or 'security'!"
            };
            _chatService.AddMessage(response);
            _activityLogger.Log($"Provided info on: {topic ?? "general"}");
        }

        private string ExtractTaskFromReminder(string input)
        {
            int startIdx = input.ToLower().IndexOf("add reminder");
            if (startIdx >= 0)
            {
                string after = input[(startIdx + "add reminder".Length)..].Trim();
                int reminderIdx = after.IndexOf("in", StringComparison.OrdinalIgnoreCase);
                if (reminderIdx > 0)
                    return after[..reminderIdx].Trim();
            }
            return string.Empty;
        }

        private string ExtractReminderText(string input)
        {
            int reminderIdx = input.ToLower().IndexOf("in", StringComparison.OrdinalIgnoreCase);
            if (reminderIdx >= 0)
            {
                return input[reminderIdx..].Trim();
            }
            return string.Empty;
        }

        // Start quiz from chat command
        private void StartQuizFromChat()
        {
            _quizEngine.StartQuiz();
            QuizQuestion.Text = _quizEngine.CurrentQuestion?.Question ?? "Quiz completed!";
            QuizAnswer.ItemsSource = _quizEngine.CurrentQuestion?.Options ?? new List<string>();
            QuizScore.Text = $"Score: {_quizEngine.Score}/10";
            StartQuizButton.IsEnabled = false;
            SubmitAnswerButton.IsEnabled = true;
            QuizAnswer.IsEnabled = true;
            _chatService.AddMessage("Chatbot: Quiz started! Check the quiz panel to answer questions.");
            _activityLogger.Log("Quiz started via chat command");
        }

        // Display all current tasks in chat
        private void ListTasksInChat()
        {
            if (_taskManager.Tasks.Count == 0)
            {
                _chatService.AddMessage("Chatbot: You have no tasks.");
                return;
            }

            _chatService.AddMessage("Chatbot: Your current tasks:");
            for (int i = 0; i < _taskManager.Tasks.Count; i++)
            {
                var task = _taskManager.Tasks[i];
                var status = task.IsCompleted ? "[✓]" : "[ ]";
                var reminder = task.Reminder.HasValue ? $" (Due: {task.Reminder.Value:dd/MM/yyyy})" : "";
                _chatService.AddMessage($"  {i + 1}. {status} {task.Title}{reminder}");
            }
        }



        // Add new task from UI form
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var taskTitle = TaskInput.Text.Trim();
            if (string.IsNullOrEmpty(taskTitle))
            {
                _chatService.AddMessage("Chatbot: Please enter a task title.");
                return;
            }

            _taskManager.AddTask(
                taskTitle,
                TaskDescriptionInput.Text.Trim(),
                ReminderInput.Text.Trim()
            );

            var task = _taskManager.Tasks[^1];
            _chatService.AddMessage($"Chatbot: Added task: {task.Title}" +
                (task.Reminder.HasValue ? $" (Reminder: {task.Reminder.Value:dd/MM/yyyy})" : ""));

            // Clear inputs
            TaskInput.Clear();
            TaskDescriptionInput.Clear();
            ReminderInput.Clear();
        }

        // Delete selected task
        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedIndex >= 0)
            {
                var task = _taskManager.Tasks[TaskList.SelectedIndex];
                _taskManager.DeleteTask(TaskList.SelectedIndex);
                _chatService.AddMessage($"Chatbot: Deleted task: {task.Title}");
                DeleteTaskButton.IsEnabled = false;
            }
        }

        // Enable/disable delete button based on selection
        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteTaskButton.IsEnabled = TaskList.SelectedIndex >= 0;
        }


        // start quiz from button
        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            _quizEngine.StartQuiz();
            QuizQuestion.Text = _quizEngine.CurrentQuestion?.Question ?? "Quiz completed!";
            QuizAnswer.ItemsSource = _quizEngine.CurrentQuestion?.Options ?? new List<string>();
            QuizScore.Text = $"Score: {_quizEngine.Score}/{_quizEngine.Questions.Count}";
            StartQuizButton.IsEnabled = false;
            SubmitAnswerButton.IsEnabled = true;
            QuizAnswer.IsEnabled = true;
        }

        // Process quiz answer submission
        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizAnswer.SelectedItem is not string selectedAnswer || _quizEngine.CurrentQuestion is null) return;

            // Extract the choice letter example A
            string letterOnly = selectedAnswer.Length >= 2 && selectedAnswer[1] == ')' ? selectedAnswer.Substring(0, 1) : selectedAnswer;

            QuizFeedback.Text = _quizEngine.CheckAnswer(letterOnly);


            QuizScore.Text = $"Score: {_quizEngine.Score}/{_quizEngine.Questions.Count}"; //totalqustions

            if (_quizEngine.HasMoreQuestions)
            {
                QuizQuestion.Text = _quizEngine.CurrentQuestion.Question;
                QuizAnswer.ItemsSource = _quizEngine.CurrentQuestion.Options;
                QuizAnswer.SelectedIndex = -1;
            }
            else
            {
                EndQuiz();
            }
        }

        // Complete quiz and show final results
        private void EndQuiz()
        {
            var result = _quizEngine.Score >= 7
                ? "Excellent! You're a cybersecurity expert!"
                : "Good effort! Keep learning about cybersecurity!";

            _chatService.AddMessage($"Chatbot: Quiz complete! {result} Score: {_quizEngine.Score}/{_quizEngine.Questions.Count}");

            QuizQuestion.Text = "Quiz completed!";
            QuizAnswer.IsEnabled = false;
            SubmitAnswerButton.IsEnabled = false;
            StartQuizButton.IsEnabled = true;
        }


        #region Activity Log
        // Show more activity log entries
        private void ShowMoreLogs_Click(object sender, RoutedEventArgs e)
        {
            _activityLogger.ShowMore();
        }
        #endregion

        // Cleanup when window closes
        protected override void OnClosed(EventArgs e)
        {
            _chatService.Dispose();
            base.OnClosed(e);
        }
    }
}
#endregion