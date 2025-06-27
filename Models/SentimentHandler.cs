namespace ChatBotFinalPoe.Models
{//sentiment handeler part 2
    public class SentimentHandler(string userName)
    {
        public string UserName { get; } = userName;

        public (string? response, string? topic) DetectSentiment(string normalizedInput)
        {
            bool isWorried = normalizedInput.Contains("worried") || normalizedInput.Contains("scared");
            bool isFrustrated = normalizedInput.Contains("frustrated") || normalizedInput.Contains("annoyed");
            bool isCurious = normalizedInput.Contains("curious") || normalizedInput.Contains("wondering");

            if (isWorried || isFrustrated || isCurious)
            {
                if (normalizedInput.Contains("password"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Let me share more tips to keep your accounts secure.", "password");
                    else if (isFrustrated)
                        return ($"I can see you’re frustrated, {UserName}. Let’s simplify password management for you.", "password");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into some password security details.", "password");
                }
                else if (normalizedInput.Contains("scam"))
                {
                    if (isWorried)
                        return ($"It’s completely understandable to feel that way, {UserName}. Scammers can be tricky, but I’ll help you stay safe.", "scam");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some ways to avoid scams.", "scam");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore how scams work and how to avoid them.", "scam");
                }
                else if (normalizedInput.Contains("privacy"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Let’s talk about keeping your privacy secure.", "privacy");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some easy privacy tips.", "privacy");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into privacy protection techniques.", "privacy");
                }
                else if (normalizedInput.Contains("phishing"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Phishing can be dangerous, but I’ll help you stay safe.", "phishing");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify how to spot phishing attempts.", "phishing");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore how phishing works and how to avoid it.", "phishing");
                }
                else if (normalizedInput.Contains("virus"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Viruses can be harmful, but I’ll guide you on protection.", "virus");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some easy virus prevention tips.", "virus");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into what viruses are and how to avoid them.", "virus");
                }
                else if (normalizedInput.Contains("malware"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Malware can be a threat, but I’ll help you stay secure.", "malware");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify malware protection for you.", "malware");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore what malware is and how to protect against it.", "malware");
                }
                else if (normalizedInput.Contains("ransomware"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Ransomware is serious, but I’ll guide you on staying safe.", "ransomware");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some ways to avoid ransomware.", "ransomware");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into how ransomware works and how to prevent it.", "ransomware");
                }
                else if (normalizedInput.Contains("firewall"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Firewalls are important, and I’ll help you understand them.", "firewall");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify how to manage your firewall.", "firewall");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore what firewalls do and how they protect you.", "firewall");
                }
                else if (normalizedInput.Contains("vpn"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. A VPN can help, and I’ll guide you on using one safely.", "vpn");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify how to use a VPN effectively.", "vpn");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into how VPNs work and their benefits.", "vpn");
                }
                else if (normalizedInput.Contains("two factor") || normalizedInput.Contains("2fa"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Two-factor authentication adds security, and I’ll help you set it up.", "twofactor");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify setting up two-factor authentication.", "twofactor");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore how two-factor authentication works.", "twofactor");
                }
                else if (normalizedInput.Contains("email scam"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Email scams can be tricky, but I’ll help you stay safe.", "emailscam");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some ways to avoid email scams.", "emailscam");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into how email scams work and how to spot them.", "emailscam");
                }
                else if (normalizedInput.Contains("update software"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Software updates are key, and I’ll guide you on staying secure.", "softwareupdate");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify keeping your software updated.", "softwareupdate");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore why software updates are important.", "softwareupdate");
                }
                else if (normalizedInput.Contains("antivirus"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Antivirus software is crucial, and I’ll help you choose the best.", "antivirus");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify using antivirus software effectively.", "antivirus");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into how antivirus software protects you.", "antivirus");
                }
                else if (normalizedInput.Contains("hacker"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Hackers can be a threat, but I’ll guide you on staying safe.", "hacker");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s go over some ways to protect against hackers.", "hacker");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore what hackers do and how to avoid them.", "hacker");
                }
                else if (normalizedInput.Contains("spam"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Spam can be risky, but I’ll help you manage it.", "spam");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify dealing with spam messages.", "spam");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s dive into what spam is and how to reduce it.", "spam");
                }
                else if (normalizedInput.Contains("trojan"))
                {
                    if (isWorried)
                        return ($"I understand you’re worried, {UserName}. Trojans can be dangerous, but I’ll guide you on staying safe.", "trojan");
                    else if (isFrustrated)
                        return ($"I can tell you’re frustrated, {UserName}. Let’s simplify protecting against Trojans.", "trojan");
                    else
                        return ($"I see you’re curious, {UserName}. Let’s explore what Trojans are and how to avoid them.", "trojan");
                }
            }
            return (null, null);
        }
    }
}