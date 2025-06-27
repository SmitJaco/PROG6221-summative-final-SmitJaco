
//the more comand to tell more about the tip
namespace ChatBotFinalPoe.Models
{
    public class ConversationFlowHandler
    {
        private readonly TopicResponses topicResponses;
        private readonly Dictionary<string, int> responseIndices = new Dictionary<string, int>
        {
            { "password", 0 }, { "scam", 0 }, { "privacy", 0 }, { "phishing", 0 }, { "virus", 0 },
            { "malware", 0 }, { "ransomware", 0 }, { "firewall", 0 }, { "vpn", 0 }, { "twofactor", 0 },
            { "emailscam", 0 }, { "softwareupdate", 0 }, { "antivirus", 0 }, { "hacker", 0 }, { "spam", 0 },
            { "trojan", 0 }
        };
        private readonly string userName;
        private string lastTopic = string.Empty;

        public ConversationFlowHandler(TopicResponses topicResponses, string userName)
        {
            this.topicResponses = topicResponses;
            this.userName = userName;
        }

        public string LastTopic
        {
            get => lastTopic;
            set => lastTopic = value;
        }

        public string? HandleConversationFlow(string normalizedInput)
        {
            if (normalizedInput.Contains("more") && !string.IsNullOrEmpty(lastTopic))
            {
                var responses = topicResponses.GetResponsesForTopic(lastTopic);
                if (responses.Count > 0)
                {
                    responseIndices[lastTopic] = (responseIndices[lastTopic] + 1) % responses.Count;
                    return $"Here’s another tip, {userName}: {responses[responseIndices[lastTopic]]}";
                }
            }
            return null;
        }
    }
}