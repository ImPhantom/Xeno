using Discord.Commands;
using System.Linq;
using System.Text;

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
                .Description("Sends user a butthurt report.")
                .Parameter("user", ParameterType.Optional)
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

            #region userinfo
            commServ.CreateCommand("userinfo")
                .Description("Gives info from user.")
                .Do(async (e) =>
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($@"**Name:** {e.User.Name}
**Nickname:** {e.User.Nickname}
**ID:** {e.User.Id}
**Joined On:** {e.User.JoinedAt}
**Avatar:** {e.User.AvatarUrl}");
                    await e.Channel.SendMessage(stringBuilder.ToString());
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

            #region userID
            commServ.CreateCommand("id")
                .Description("Gets a users ID")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage($@"**User ID:** {e.User.Id}
**Text Channel ID:** {e.Channel.Id}");
                });
            #endregion
        }
    }
}
