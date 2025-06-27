namespace ChatBotFinalPoe.Models
{//handels basic interactions hi hello what is purpuse etc
    public class BasicInteractionHandler(string userName)
    {
        public string UserName { get; } = userName;

        public string? HandleBasicInteractions(string normalizedInput)
        {
            if (normalizedInput == "hi" || normalizedInput == "hello" || normalizedInput.StartsWith("hi ", StringComparison.OrdinalIgnoreCase) || normalizedInput.StartsWith("hello ", StringComparison.OrdinalIgnoreCase) || normalizedInput.Contains(" hi ") || normalizedInput.Contains(" hello "))
            {
                return $"Hi there, {UserName}! I am Ultimate Chatbot, here to help you stay safe online. What's on your mind?";
            }
            else if (normalizedInput.Contains("how are you"))
            {
                return $"I’m doing great, thanks for asking, {UserName}! I am Ultimate Chatbot, here to assist with cybersecurity tips.";
            }
            else if (normalizedInput.Contains("what's your purpose") || normalizedInput.Contains("what is your purpose"))
            {
                return $"I am Ultimate Chatbot, and my purpose is to educate you on cybersecurity and help you stay safe online, {UserName}!";
            }
            else if (normalizedInput.Contains("what can i ask about") || normalizedInput.Contains("what can i ask"))
            {
                return $"You can ask about staying safe online, like password safety, phishing, safe browsing, and much more, {UserName}! I am Ultimate Chatbot, ready to help.";
            }
            else if (normalizedInput.Contains("who are you"))
            {
                return $"I am Ultimate Chatbot, your go-to assistant for cybersecurity education and online safety tips, {UserName}!";
            }
            else if (normalizedInput.Contains("thanks") || normalizedInput.Contains("thank you"))
            {
                return $"You’re welcome, {UserName}! I am Ultimate Chatbot, always here to help with your cybersecurity questions.";
            }
            else if (normalizedInput.Contains("bye") || normalizedInput.Contains("goodbye"))
            {
                return $"See you later, {UserName}! I am Ultimate Chatbot, stay safe online!";
            }

            return null;
        }
    }
}