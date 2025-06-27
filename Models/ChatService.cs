using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Media;
using System.Windows;
//welcome message ascii art and audio
namespace ChatBotFinalPoe.Models
{
    public class ChatService : IDisposable
    {
        private SoundPlayer? _soundPlayer;
        public ObservableCollection<string> ChatMessages { get; } = [];

        public void AddMessage(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChatMessages.Add(message);
            });
        }

        public void DisplayWelcome()
        {
            string logo = @"
       _ _   _                 _         _____  _   _   ___ _____  ______  _____ _____   _ _ 
      | | | (_)               | |       /  __ \| | | | / _ \_   _| | ___ \|  _  |_   _| | | |
 _   _| | |_ _ _ __ ___   __ _| |_ ___  | /  \/| |_| |/ /_\ \| |   | |_/ /| | | | | |   | | |
| | | | | __| | '_ ` _ \ / _` | __/ _ \ | |    |  _  ||  _  || |   | ___ \| | | | | |   | | |
| |_| | | |_| | | | | | | (_| | ||  __/ | \__/\| | | || | | || |   | |_/ /\ \_/ / | |   |_|_|
 \__,_|_|\__|_|_| |_| |_|\__,_|\__\___|  \____/\_| |_/\_| |_/\_/   \____/  \___/  \_/   (_|_)
                                                                                             
                                                                                             
";
            AddMessage(logo);
            PlayWelcomeSound();
            AddMessage("Chatbot: Hello I am Ultimate Chatbot. What is your Name?");
        }

        private void PlayWelcomeSound()
        {
            try
            {
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "welcome.wav");

                if (File.Exists(audioPath))
                {
                    _soundPlayer?.Dispose();
                    _soundPlayer = new SoundPlayer(audioPath);
                    _soundPlayer.Play();
                }
            }
            catch {  }
        }

        public void Dispose()
        {
            _soundPlayer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}