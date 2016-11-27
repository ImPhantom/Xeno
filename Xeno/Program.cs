using Discord;
using Discord.Commands;
using System;
using System.Configuration;

namespace Xeno
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();
        }

        public static DiscordClient client;
        public static bool debug = false;

        private string header = @"             
                                            __   __                
                                            \ \ / /                
                                             \ V / ___ _ __   ___  
                                              > < / _ \ '_ \ / _ \ 
                                             / ^ \  __/ | | | (_) |
                                            /_/ \_\___|_| |_|\___/ 
        -----------------------------------------------------------------------------------------------------
                                                         ";

        public void Start()
        {
            Console.WriteLine(header);
            Console.Title = Strings.appName;

            if (Strings.botToken == "bot token")
            {
                Console.WriteLine(Strings.tokenError);
                Console.ReadLine();
            } else
            {
                client = new DiscordClient(x =>
                {
                    x.AppName = Strings.appName;
                    x.AppUrl = Strings.appUrl;
                    if (debug == false)
                    {
                        x.LogLevel = LogSeverity.Info;
                    }
                    else
                    {
                        x.LogLevel = LogSeverity.Debug;
                    }
                    x.LogHandler = Log;
                });

                client.UsingCommands(x =>
                {
                    x.PrefixChar = Strings.prefixChar;
                    x.AllowMentionPrefix = false;
                    x.HelpMode = HelpMode.Public;
                });


                Events.initEvents(); // Events
                Console.WriteLine(Strings.infoPrefix + "Events module initialized.");

                Settings.initSettings(); // Settings
                Console.WriteLine(Strings.infoPrefix + "Settings module initialized.");
                Moderation.initModeration(); // Moderation
                Console.WriteLine(Strings.infoPrefix + "Moderation module initialized.");
                Chat.initChat(); // Chat
                Console.WriteLine(Strings.infoPrefix + "Chat module initialized.");

                Console.WriteLine(Strings.infoPrefix + "Connecting...");
                client.ExecuteAndWait(async () =>
                {
                    await client.Connect(Strings.botToken, TokenType.Bot);
                    client.SetGame(Strings.startStatus);
                });
            }
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }
    }
}
