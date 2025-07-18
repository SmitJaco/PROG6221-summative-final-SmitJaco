﻿using System;
using System.Linq;

namespace ChatBotFinalPoe.Models
{
    public static class NlpProcessor
    {//synonyms for comands
        private static readonly string[] Greetings =
        {
            "hello", "hi", "hey",
            "good morning", "good afternoon", "good evening",
            "greetings", "howdy", "what's up"
        };

        private static readonly string[] TaskVerbs =
        {
            "add", "create", "make", "new", "set",
            "insert", "record", "establish", "build"
        };

        private static readonly string[] TaskNouns =
        {
            "task", "todo", "action item",
            "job", "chore", "duty", "assignment"
        };

        private static readonly string[] ReminderTriggers =
        {
            "remind", "alert", "notify", "warn",
            "remember", "ping"
        };

        private static readonly string[] QuizTriggers =
        {
            "quiz", "test", "exam", "questions",
            "challenge", "game", "trivia"
        };

        private static readonly string[] InfoVerbs =
        {
            "tell", "give", "show", "explain", "provide"
        };

        private static readonly string[] InfoTopics =
        {
            "tips", "information", "details",

        };

        public enum CommandType
        {
            None,
            AddTask,
            AddReminder,
            StartQuiz,
            ListTasks,
            InfoRequest
        }

        public static (bool IsCommand, CommandType Command, string? TaskTitle, string? Reminder, string? Topic) ProcessTaskCommand(string input)
        {
            string lowerInput = input.ToLower();

            // Check for greetings 
            if (Greetings.Any(g => lowerInput.Trim().Equals(g)))
                return (false, CommandType.None, null, null, null);

            // Check for informational requests
            if (InfoVerbs.Any(v => lowerInput.Contains(v)) && InfoTopics.Any(t => lowerInput.Contains(t)))
            {
                string topic = InfoTopics.FirstOrDefault(t => lowerInput.Contains(t)) ?? "general";
                return (true, CommandType.InfoRequest, null, null, topic);
            }

            // Check for task creation
            if (TaskVerbs.Any(v => lowerInput.Contains(v)) && TaskNouns.Any(n => lowerInput.Contains(n)))
            {
                return (true, CommandType.AddTask, ExtractTaskTitle(input), null, null);
            }

            // Check for reminders
            if (ReminderTriggers.Any(r => lowerInput.Contains(r)) || lowerInput.Contains("add reminder"))
            {
                var (task, reminder) = ExtractTaskAndReminder(input);
                return (true, CommandType.AddReminder, task, reminder, null);
            }

            // Check for quiz start
            if ((QuizTriggers.Any(q => lowerInput.Contains(q)) && (lowerInput.Contains("start") || lowerInput.Contains("begin"))) ||
                lowerInput.Contains("start quiz"))
            {
                return (true, CommandType.StartQuiz, null, null, null);
            }

            // Check for task listing
            if (lowerInput.Contains("list tasks") || lowerInput.Contains("show tasks"))
            {
                return (true, CommandType.ListTasks, null, null, null);
            }

            return (false, CommandType.None, null, null, null);
        }

        public static bool IsGreeting(string input)
        {
            return Greetings.Any(g => input.Trim().Equals(g, StringComparison.OrdinalIgnoreCase));
        }

        private static (string Task, string Reminder) ExtractTaskAndReminder(string input)
        {
            foreach (var trigger in ReminderTriggers)
            {
                int triggerIndex = input.IndexOf(trigger, StringComparison.OrdinalIgnoreCase);
                if (triggerIndex >= 0)
                {
                    string beforeTrigger = input[..triggerIndex].Trim();
                    string afterTrigger = input[(triggerIndex + trigger.Length)..].Trim();

                    // Try to extract task from before or after the trigger
                    string task = !string.IsNullOrEmpty(beforeTrigger) ? beforeTrigger : afterTrigger;
                    int timeIndex = afterTrigger.IndexOf(" in ", StringComparison.OrdinalIgnoreCase);
                    int dateIndex = afterTrigger.IndexOf(" on ", StringComparison.OrdinalIgnoreCase);

                    if (timeIndex >= 0)
                    {
                        // Safe check before using IndexOf result
                        int taskInIndex = task.IndexOf(" in ", StringComparison.OrdinalIgnoreCase);
                        if (taskInIndex >= 0)
                        {
                            task = task[..taskInIndex].Trim();
                        }
                        string reminder = afterTrigger[(timeIndex + 4)..].Trim();
                        return (task, reminder);
                    }
                    else if (dateIndex >= 0)
                    {
                        // Safe check before using IndexOf result
                        int taskOnIndex = task.IndexOf(" on ", StringComparison.OrdinalIgnoreCase);
                        if (taskOnIndex >= 0)
                        {
                            task = task[..taskOnIndex].Trim();
                        }
                        string reminder = afterTrigger[(dateIndex + 4)..].Trim();
                        return (task, reminder);
                    }

                    return (task, "today");
                }
            }
            return ("", "");
        }

        private static string ExtractTaskTitle(string input)
        {
            foreach (var verb in TaskVerbs)
            {
                foreach (var noun in TaskNouns)
                {
                    int verbIndex = input.IndexOf(verb, StringComparison.OrdinalIgnoreCase);
                    int nounIndex = input.IndexOf(noun, StringComparison.OrdinalIgnoreCase);

                    if (verbIndex >= 0 && nounIndex >= 0)
                    {
                        int start = Math.Max(verbIndex, nounIndex) + Math.Max(verb.Length, noun.Length);
                        return input[start..].Trim();
                    }
                }
            }
            return string.Empty;
        }
    }
}