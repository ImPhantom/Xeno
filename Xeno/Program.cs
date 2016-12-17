using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            Console.WriteLine(header);
            Console.Title = "Xeno Discord Bot";

            ConfigCheck();

            client = new DiscordClient(x =>
            {
                x.AppName = "Xeno";
                x.AppUrl = "http://bot.xenorp.com/";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = Configuration.Load().cmdPrefix;
                x.AllowMentionPrefix = false;
                x.HelpMode = HelpMode.Public;
            })
            .UsingPermissionLevels((u, c) => (int)GetPermission(u, c))
            .UsingModules();


            Events.initEvents(); // Event Module

            client.AddModule<Settings>("Settings", ModuleFilter.None);
            client.AddModule<Chat>("Chat", ModuleFilter.None);
            client.AddModule<Moderation>("Moderation", ModuleFilter.None);

            client.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    try
                    {
                        await client.Connect(Configuration.Load().botToken, TokenType.Bot);
                        break;
                    }
                    catch (Exception e)
                    {
                        client.Log.Error("Connection Attempt Failed.", e);

                    }
                }
            });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }

        private void ConfigCheck()
        {
            if (!Directory.Exists("cfg"))
                Directory.CreateDirectory("cfg");

            string location = "cfg/config.json";

            if(!File.Exists(location))
            {
                var config = new Configuration();
                Console.WriteLine("The config file has been made at: 'cfg\\config.json', \n Please enter your information and restart the bot.");
                Console.Write("Token: ");

                config.botToken = Console.ReadLine();
                config.Save();
            }
            Console.WriteLine(Strings.infoEvent + "Config Loaded!");
        }

        private PermLevel GetPermission(User user, Channel channel)
        {
            if (user.IsBot)
                return PermLevel.User;

            if (Configuration.Load().botOwners.Contains(user.Id))
                return PermLevel.BotOwner;

            if(!channel.IsPrivate)
            {
                if (user == channel.Server.Owner)
                    return PermLevel.ServerOwner;
                if (user.ServerPermissions.Administrator)
                    return PermLevel.ServerAdmin;
                if (user.GetPermissions(channel).ManageChannel)
                    return PermLevel.ChannelAdmin;
            }
            return PermLevel.User;
        }
    }
}
