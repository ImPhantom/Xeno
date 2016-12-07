using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using Xeno.Utilities;

namespace Xeno.Modules
{
    class Chat
    {
        public static void initChat()
        {
            var client = Program.client;
            var commServ = client.GetService<CommandService>();

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
                .Description("Gives info from server.")
                .Do(async (e) =>
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($@"**Name:** {e.Server.Name}
**Owner:** {e.Server.Owner}
**ID:** {e.Server.Id}
**Region:** {e.Server.Region.Hostname}
**Total Users:** {e.Server.UserCount}
**Roles:** {e.Server.RoleCount}
**Text Channels:** {e.Server.TextChannels.Count()} **Voice Channels:** {e.Server.VoiceChannels.Count()}
**AFK Channel:** {e.Server.AFKChannel}
**Icon:** {e.Server.IconUrl}");
                    await e.Channel.SendMessage(stringBuilder.ToString());
                });
            #endregion

            #region uinfo
            commServ.CreateCommand("uinfo")
                .Description("Provides the sender with either their info or if specified another users info.")
                .Parameter("user", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if(e.Args[0] == String.Empty)
                    {
                        await e.Channel.SendMessage(Usr.getUserInfo(Usr.getUser(e), e, true));
                    } else
                    {
                        await e.Channel.SendMessage(Usr.getUserInfo(Usr.getUser(e), e, false));
                    }
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

        }
    }
}
