using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using Xeno.Modules;
using Xeno.Utilities;

namespace Xeno
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();
        }

        public static DiscordClient client;

        private string header = @"          
        -----------------------------------------------------------------------------------------------------   
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
            String command;
            Console.WriteLine(header);
            Console.Title = "Xeno Discord Bot";

            if (String.IsNullOrEmpty(Strings.botToken))
            {
                Console.WriteLine(Strings.tokenError);
                command = Console.ReadLine();
                switch (command)
                {
                    case "config":
                        Console.WriteLine("Opened Config. (edit: Xeno.exe.config)");
                        Process.Start($@"{AppDomain.CurrentDomain.BaseDirectory}");
                        break;
                }
            } else
            {
                client = new DiscordClient(x =>
                {
                    x.AppName = "Xeno";
                    x.AppUrl = "http://bot.xenorp.com/";
                    x.LogLevel = LogSeverity.Info;
                    x.LogHandler = Log;
                });

                client.UsingCommands(x =>
                {
                    x.PrefixChar = Strings.prefixChar;
                    x.AllowMentionPrefix = false;
                    x.HelpMode = HelpMode.Private;
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
                    command = Console.ReadLine();
                    switch (command)
                    {
                        case "config":
                            Console.WriteLine("Opened Config.");
                            Process.Start($@"{AppDomain.CurrentDomain.BaseDirectory}");
                            break;
                    }
                });
            }
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }
    }
}
