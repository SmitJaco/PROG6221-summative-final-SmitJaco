namespace ChatBotFinalPoe.Models
{//responses to topics part 1
    public class TopicResponses
    {
        private readonly Random random = new Random();
        private readonly Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "Make sure to use unique passwords for each account. Avoid using personal details in your passwords.",
                "A strong password should be at least 12 characters long with a mix of letters, numbers, and symbols.",
                "Consider using a password manager to generate and store complex passwords securely.",
                "Change your passwords every few months to enhance security.",
                "Avoid common words or phrases; mix in special characters for added strength."
            },
            ["scam"] = new List<string>
            {
                "Scams often use urgent language to trick you. Take a moment to verify before acting.",
                "If an offer seems too good to be true, it’s likely a scam. Stay cautious!",
                "Never share personal info with unsolicited messages. Scammers can be very convincing.",
                "Check for poor grammar or unusual email domains as red flags for scams.",
                "Report suspicious messages to your email provider to help stop scammers."
            },
            ["privacy"] = new List<string>
            {
                "Use strong passwords, enable 2FA, and avoid sharing personal info on social media to protect your privacy.",
                "Clear your browser cookies regularly and use private browsing modes to enhance your privacy.",
                "Consider using a VPN to encrypt your internet connection and keep your online activity private.",
                "Limit the amount of personal data you share on public profiles.",
                "Review app permissions to ensure they only access what’s necessary."
            },
            ["phishing"] = new List<string>
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                "Phishing attacks often come as fake emails or texts. Never click on suspicious links!",
                "Always verify the sender’s email address before responding. Phishing scams can look very convincing.",
                "Hover over links to check their true destination before clicking.",
                "Use email filters to move potential phishing attempts to spam."
            },
            ["virus"] = new List<string>
            {
                "A virus is malicious software that can infect your device, steal data, or cause harm. Always use antivirus software to protect yourself.",
                "Avoid downloading files from untrusted sources to prevent virus infections.",
                "Keep your operating system updated to patch vulnerabilities that viruses exploit.",
                "Run regular scans with your antivirus to detect and remove viruses early.",
                "Be wary of email attachments, as they’re a common virus delivery method."
            },
            ["malware"] = new List<string>
            {
                "Malware is harmful software like viruses, worms, or ransomware that can damage your device or steal data. Be cautious with downloads!",
                "Install reputable security software to defend against various types of malware.",
                "Avoid clicking on pop-up ads, which can install malware silently.",
                "Monitor your device for unusual slowdowns, a sign of possible malware infection.",
                "Educate yourself on phishing to reduce malware risks from deceptive emails."
            },
            ["ransomware"] = new List<string>
            {
                "Ransomware locks your device or files and demands payment to unlock them. Back up your data regularly to avoid losing it.",
                "Enable real-time protection on your antivirus to block ransomware attempts.",
                "Avoid opening attachments from unknown senders to prevent ransomware infections.",
                "Store backups offline to ensure ransomware can’t encrypt them.",
                "Never pay the ransom, as it doesn’t guarantee data recovery."
            },
            ["firewall"] = new List<string>
            {
                "A firewall protects your device by blocking unauthorized access. Make sure it’s enabled on your system!",
                "Configure your firewall to allow only trusted applications.",
                "Check firewall logs regularly to spot unusual activity.",
                "Use both hardware and software firewalls for layered security.",
                "Update your firewall settings when installing new software."
            },
            ["vpn"] = new List<string>
            {
                "A VPN encrypts your internet connection, keeping your online activity private. It’s great for public Wi-Fi!",
                "Choose a reputable VPN provider with a no-logs policy.",
                "Use a VPN when accessing sensitive accounts on public networks.",
                "Turn on your VPN automatically to ensure constant protection.",
                "Avoid free VPNs, as they may compromise your privacy."
            },
            ["twofactor"] = new List<string>
            {
                "Two-factor authentication (2FA) adds an extra layer of security by requiring a second verification step, like a code sent to your phone.",
                "Set up 2FA using an authenticator app for added security.",
                "Use backup codes provided by 2FA services in case you lose access.",
                "Enable 2FA on all accounts that support it to maximize protection.",
                "Consider hardware tokens for an even stronger 2FA option."
            },
            ["emailscam"] = new List<string>
            {
                "Email scams often trick you into sharing personal info. Look for spelling errors, suspicious links, or urgent requests.",
                "Verify the sender’s identity by contacting them through official channels.",
                "Mark scam emails as spam to help your email provider learn.",
                "Avoid replying to or engaging with scam emails to avoid escalation.",
                "Use a separate email for public registrations to reduce scam exposure."
            },
            ["softwareupdate"] = new List<string>
            {
                "Keeping your software updated patches security vulnerabilities. Enable automatic updates whenever possible!",
                "Check for updates manually if automatic updates are disabled.",
                "Update all apps, not just your operating system, to close security gaps.",
                "Restart your device after updates to ensure changes take effect.",
                "Subscribe to security alerts for timely update notifications."
            },
            ["antivirus"] = new List<string>
            {
                "Antivirus software protects against malware by scanning and removing threats. Make sure it’s up to date!",
                "Schedule regular scans to catch threats that slip through.",
                "Use real-time protection to block malware before it executes.",
                "Choose an antivirus with a good reputation for detection rates.",
                "Avoid disabling antivirus unless absolutely necessary for troubleshooting."
            },
            ["hacker"] = new List<string>
            {
                "A hacker is someone who uses technical skills to gain unauthorized access to systems. Some are malicious, others help improve security.",
                "Ethical hackers help companies find and fix vulnerabilities.",
                "Malicious hackers often target outdated software for easy access.",
                "Stay informed about common hacking techniques to protect yourself.",
                "Report hacking attempts to authorities if they occur."
            },
            ["spam"] = new List<string>
            {
                "Spam is unwanted messages, often containing scams or malware. Use filters and don’t click on suspicious links.",
                "Set up email rules to automatically delete spam.",
                "Avoid subscribing to unknown mailing lists to reduce spam.",
                "Use a secondary email for less critical communications.",
                "Report spam to your email provider to improve filtering."
            },
            ["trojan"] = new List<string>
            {
                "A Trojan is a type of malware disguised as legitimate software. It can steal data or harm your device.",
                "Avoid downloading software from unverified websites to prevent Trojans.",
                "Look for unusual behavior after new installations as a Trojan sign.",
                "Use antivirus to detect and remove Trojans from your system.",
                "Be cautious with free software, a common Trojan vector."
            }
        };

        public string? GetRandomResponse(string topic)
        {
            if (topicResponses.ContainsKey(topic))
            {
                var responses = topicResponses[topic];
                return responses[random.Next(responses.Count)];
            }
            return null;
        }

        public List<string> GetResponsesForTopic(string topic)
        {
            return topicResponses.ContainsKey(topic) ? topicResponses[topic] : new List<string>();
        }
    }
}