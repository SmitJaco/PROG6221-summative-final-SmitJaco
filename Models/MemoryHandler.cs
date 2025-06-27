namespace ChatBotFinalPoe.Models
{//memory handeler handles intrists and user name
    public class MemoryHandler
    {
        private readonly Dictionary<string, string> userMemory = new Dictionary<string, string>();
        private readonly string userName;

        public MemoryHandler(string userName)
        {
            this.userName = userName;
            userMemory["name"] = userName;
        }

        public string? HandleMemoryAndRecall(string normalizedInput)
        {
            if (normalizedInput.Contains("interested in") || normalizedInput.Contains("i care about"))
            {
                string interest = string.Empty;
                if (normalizedInput.Contains("password"))
                {
                    interest = "password security";
                }
                else if (normalizedInput.Contains("scam"))
                {
                    interest = "avoiding scams";
                }
                else if (normalizedInput.Contains("privacy"))
                {
                    interest = "online privacy";
                }
                else if (normalizedInput.Contains("phishing"))
                {
                    interest = "phishing protection";
                }
                else
                {
                    string[] words = normalizedInput.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if ((words[i] == "interested" || words[i] == "care") && i + 2 < words.Length)
                        {
                            interest = words[i + 2];
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(interest))
                {
                    userMemory["interest"] = interest;
                    return $"Got it, {userName}! I’ll remember that you’re interested in {interest}. Let’s talk more about it.";
                }
            }

            if (normalizedInput.Contains("what do you know about me") && userMemory.ContainsKey("interest"))
            {
                return $"I remember you’re interested in {userMemory["interest"]}, {userName}. Would you like more tips on that?";
            }

            return null;
        }
    }
}