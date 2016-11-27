using Discord;
using Discord.Commands;
using System;

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

        private string token = "MjQzMjY0MDk3MDg3NTIwNzY4.CxZG6A.Z74vR-bLKrOC-gSmCFJAiH7zgbs";
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
            Console.Title = "Xeno Discord Bot";

            client = new DiscordClient(x =>
            {
                x.AppName = "Xenos";
                x.AppUrl = "http://xenoserv.cf/";
                if(debug == false)
                {
                    x.LogLevel = LogSeverity.Info;
                } else
                {
                    x.LogLevel = LogSeverity.Debug;
                }
                x.LogHandler = Log;
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = '~';
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
                await client.Connect(token, TokenType.Bot);
                client.SetGame(Strings.status);
            });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }
    }
}
