namespace ChatBotFinalPoe.Models
{//response handeler part 2 part 1 minor updates to part 3
    public class ResponseHandler
    {
        public SentimentHandler SentimentHandler { get; }
        public MemoryHandler MemoryHandler { get; }
        public BasicInteractionHandler BasicInteractionHandler { get; }
        public ConversationFlowHandler ConversationFlowHandler { get; }
        public TopicResponses TopicResponses { get; }

        public ResponseHandler(string userName)
        {
            SentimentHandler = new SentimentHandler(userName);
            MemoryHandler = new MemoryHandler(userName);
            BasicInteractionHandler = new BasicInteractionHandler(userName);
            ConversationFlowHandler = new ConversationFlowHandler(new TopicResponses(), userName);
            TopicResponses = new TopicResponses();
        }

        public string? GetResponse(string input)
        {
            string normalizedInput = input.ToLower();

            // Call SentimentHandler
            var (sentimentResponse, sentimentTopic) = SentimentHandler.DetectSentiment(normalizedInput);
            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                ConversationFlowHandler.LastTopic = sentimentTopic ?? string.Empty;
                return sentimentResponse;
            }

            // Call MemoryHandler
            string? memoryResponse = MemoryHandler.HandleMemoryAndRecall(normalizedInput);
            if (!string.IsNullOrEmpty(memoryResponse))
            {
                return memoryResponse;
            }

            // Call BasicInteractionHandler
            string? basicResponse = BasicInteractionHandler.HandleBasicInteractions(normalizedInput);
            if (!string.IsNullOrEmpty(basicResponse))
            {
                return basicResponse;
            }

            // Call ConversationFlowHandler
            string? flowResponse = ConversationFlowHandler.HandleConversationFlow(normalizedInput);
            if (!string.IsNullOrEmpty(flowResponse))
            {
                return flowResponse;
            }

            // Call TopicResponses via a topic check
            string topic = GetTopicFromInput(normalizedInput);
            if (!string.IsNullOrEmpty(topic))
            {
                ConversationFlowHandler.LastTopic = topic;
                return TopicResponses.GetRandomResponse(topic) ?? $"Sorry, {SentimentHandler.UserName}, I have some tips on that topic! Ask for 'more' to learn additional details.";
            }

            // Default response
            return $"Sorry, {SentimentHandler.UserName}, I didn’t understand that. Try asking about passwords, phishing, or say 'hi' to start!";
        }

        private static string GetTopicFromInput(string input)
        {
            string[] topics = new[] { "password", "scam", "privacy", "phishing", "virus", "malware", "ransomware", "firewall", "vpn", "twofactor", "emailscam", "softwareupdate", "antivirus", "hacker", "spam", "trojan" };
            return topics.FirstOrDefault(topic => input.Contains(topic)) ?? string.Empty;
        }
    }
}