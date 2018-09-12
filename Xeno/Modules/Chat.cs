using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using System;
using System.Linq;
using System.Text;
using Xeno.Utilities;

namespace Xeno.Modules
{
    public class Chat : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands("", commServ =>
            {
                #region butthurt
                commServ.CreateCommand("butthurt")
                    .Description("Sends specified user a butthurt report.")
                    .Parameter("user", ParameterType.Unparsed)
                    .Do(async (e) =>
                    {
                        await e.Channel.SendMessage($"http://i.imgur.com/jhiVEpv.png");
                        await e.Channel.SendMessage($"{e.GetArg("user")} Please fill out this butthurt report.");
                    });
                #endregion

                #region info
                commServ.CreateCommand("info")
                    .Description("Provides the sender with the servers info.")
                    .Do(async (e) =>
                    {
                        await e.Channel.SendMessage(Svr.getServerInfo(e));
                    });
                #endregion

                #region uinfo
                commServ.CreateCommand("uinfo")
                    .Description("Provides the sender with either their info or if specified another users info.")
                    .Parameter("user", ParameterType.Unparsed)
                    .Do(async (e) =>
                    {
                        /*if (e.Args[0] == string.Empty)
                        {
                        }
                        else
                        {
                            await e.Channel.SendMessage(Usr.getUserInfo(Usr.getUser(e), e, false));
                        }*/
                        if(e.Args[0] != string.Empty)
                            await e.Channel.SendMessage(Usr.getUserInfo(Usr.getUser(e), e, false));
                    });
                #endregion

                #region bot
                commServ.CreateCommand("bot")
                    .Description("Sends the bot info to the user.")
                    .Do(async (e) =>
                    {
                        await e.User.PrivateChannel.SendMessage($@"**Xeno is created and maintained by ImPhantom**
**Xeno is still in *ALPHA*, So expect issues.**
**GitHub:** https://github.com/ImPhantom/Xeno
**Downloads:** http://bot.xenorp.com/");
                    });
                #endregion
            });
        }
    }
}
