using System.Collections.Generic;
//constructor
namespace ChatBotFinalPoe.Models
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }

        public QuizQuestion(string question, List<string> options, string correctAnswer)
        {
            Question = question;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }
}