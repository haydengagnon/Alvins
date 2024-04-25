using DiscordWebhook;

namespace common
{
    public class DiscordLogging
    {
        private Webhook CommandLog = new Webhook("https://discordapp.com/api/webhooks/628287220222263306/mC-3j1pXbp0EEYoP9Rs4d-LFObCpAP_VemJZz1LLC7rW26d7bIxCD7HXEyEVLebTBEY5");
        private Webhook ModClientLog = new Webhook("https://discordapp.com/api/webhooks/657903406173650964/5NXBCU2erZvihbP-OdtlqhK6eHUQC_ge1qPI_2QPs3esLzRTWcx6CnU2ZSqoEoUIR853");

        string[] HackerCache = new string[5];
        int HC = 0;

        public void Log2Discord(string info)
        {
            HackerCache[HC] = info;
            HC++;
            if (HC == 5)
            {
                //Console.WriteLine($"[{string.Join(",", HackerCache)}]");
                Send2Discord(ModClientLog, "Haxors Log", $"[{string.Join(",", HackerCache)}]");
                HackerCache = new string[5];
                HC = 0;
            }
        }

        public void Send2Discord(Webhook hook, string name, string content)
        {
            if (!ServerConfig.EnableDebug)
            {
                hook.PostData(new WebhookObject()
                {
                    username = name,
                    content = content
                });
            }
        }
    }
}
