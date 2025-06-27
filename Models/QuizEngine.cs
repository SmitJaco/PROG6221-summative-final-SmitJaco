using System.Collections.ObjectModel;
using System.ComponentModel;
//engine that determins right and wrong answers in quiz game also give quistions and tips
namespace ChatBotFinalPoe.Models
{
    public class QuizEngine : INotifyPropertyChanged
    {
        private int _currentQuestionIndex = -1;
        private int _score = 0;
        private QuizQuestion? _currentQuestion;

        public ObservableCollection<QuizQuestion> Questions { get; } = new()
        {
            new QuizQuestion("What should you do if you receive an email asking for your password?",
                new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" }, "C"),
            new QuizQuestion("True or False: A strong password should include numbers and symbols.",
                new List<string> { "True", "False" }, "True"),
            new QuizQuestion("What is a common sign of a phishing email?",
                new List<string> { "A) Professional design", "B) Spelling errors", "C) Official logo", "D) Clear sender address" }, "B"),
            new QuizQuestion("True or False: Public Wi-Fi is always secure.",
                new List<string> { "True", "False" }, "False"),
            new QuizQuestion("What does 2FA stand for?",
                new List<string> { "A) Two Factor Authentication", "B) Two File Access", "C) Two Firewall Access", "D) Two Format Authorization" }, "A"),
            new QuizQuestion("True or False: Updating software can fix security vulnerabilities.",
                new List<string> { "True", "False" }, "True"),
            new QuizQuestion("What should you avoid clicking in unsolicited emails?",
                new List<string> { "A) Links", "B) Attachments", "C) Images", "D) All of the above" }, "D"),
            new QuizQuestion("True or False: Antivirus software can detect and block malware.",
                new List<string> { "True", "False" }, "True"),
            new QuizQuestion("What is a key benefit of using a VPN?",
                new List<string> { "A) Faster internet", "B) Encrypted connection", "C) More storage", "D) Better graphics" }, "B"),
            new QuizQuestion("True or False: Sharing passwords with friends is safe.",
                new List<string> { "True", "False" }, "False"),
            new QuizQuestion("What is the best way to secure your online accounts?",
                new List<string> { "A) Use the same password everywhere", "B) Enable two-factor authentication", "C) Share passwords with family", "D) Avoid updating software" }, "B"),
            new QuizQuestion("True or False: It’s safe to download files from unknown websites.",
                new List<string> { "True", "False" }, "False")
        };

        public QuizQuestion? CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }

        public bool HasMoreQuestions => _currentQuestionIndex >= 0 && _currentQuestionIndex < Questions.Count - 1;

        public int Score => _score;

        public void StartQuiz()
        {
            _currentQuestionIndex = 0;
            _score = 0;
            CurrentQuestion = Questions[_currentQuestionIndex];
            OnPropertyChanged(nameof(Score));
        }

        public string CheckAnswer(string userAnswer)
        {//check if quiz started
            if (CurrentQuestion == null) return "Quiz not properly initialized";

            string normalizedUserAnswer = userAnswer.Trim();
            string normalizedCorrectAnswer = CurrentQuestion.CorrectAnswer.Trim();

            bool isCorrect = normalizedUserAnswer.Equals(normalizedCorrectAnswer, StringComparison.OrdinalIgnoreCase);
            var currentQ = CurrentQuestion;

            _currentQuestionIndex++;
            //check if correct
            if (isCorrect)
            {
                _score++;
                OnPropertyChanged(nameof(Score));
            }

            if (HasMoreQuestions)
            {
                CurrentQuestion = Questions[_currentQuestionIndex];
            }
            else
            {
                CurrentQuestion = null;
            }

            return isCorrect
                ? $"Correct! {GetEducationalTip(currentQ)}"
                : $"Incorrect! The right answer was {currentQ.CorrectAnswer}. {GetEducationalTip(currentQ)}";
        }
        //tips for answers
        private static string GetEducationalTip(QuizQuestion question)
        {
            return question.Question switch
            {
                "What should you do if you receive an email asking for your password?" =>
                    "Never share your password via email. Always report phishing emails to your IT/security team.",
                "True or False: A strong password should include numbers and symbols." =>
                    "Strong passwords use a mix of uppercase letters, lowercase letters, numbers, and special characters.",
                "What is a common sign of a phishing email?" =>
                    "Phishing emails often contain spelling mistakes or poor grammar. Stay alert!",
                "True or False: Public Wi-Fi is always secure." =>
                    "Public Wi-Fi can be risky. Use a VPN to protect your data when browsing in public places.",
                "What does 2FA stand for?" =>
                    "Two-Factor Authentication adds an extra layer of security by requiring a second verification method.",
                "True or False: Updating software can fix security vulnerabilities." =>
                    "Always update your software. Updates often patch known security issues.",
                "What should you avoid clicking in unsolicited emails?" =>
                    "Avoid clicking links, downloading attachments, or viewing images in suspicious emails.",
                "True or False: Antivirus software can detect and block malware." =>
                    "Antivirus software helps protect your system, but it must be updated regularly.",
                "What is a key benefit of using a VPN?" =>
                    "A VPN encrypts your internet traffic, protecting your data from hackers and trackers.",
                "True or False: Sharing passwords with friends is safe." =>
                    "Never share your passwords. Keep them private to protect your accounts.",
                "What is the best way to secure your online accounts?" =>
                    "Enable two-factor authentication for extra protection, even if your password gets stolen.",
                "True or False: It’s safe to download files from unknown websites." =>
                    "Avoid downloading from unknown sites. They may host viruses or malware.",
                _ => "Cybersecurity is everyone’s responsibility. Stay informed and stay secure!"
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
