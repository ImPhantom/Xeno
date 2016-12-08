using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;

namespace Xeno.Utilities
{
    class Svr
    {
        public static string getServerInfo(CommandEventArgs e)
        {
            var svrInfo = new StringBuilder();
            svrInfo.AppendLine($@"**Name:** {e.Server.Name}
**Owner:** {e.Server.Owner}
**ID:** `{e.Server.Id}`
**Region:** `{e.Server.Region.Hostname}`
**Total Users:** {e.Server.UserCount}
**Roles:** {e.Server.RoleCount}
**Text Channels:** {e.Server.TextChannels.Count()} **Voice Channels:** {e.Server.VoiceChannels.Count()}
**AFK Channel:** {e.Server.AFKChannel}
**Icon:** {e.Server.IconUrl}");
            return svrInfo.ToString();
        }

        public static Channel getLogChannel(ChannelEventArgs ch)
        {
            var log = ch.Server.FindChannels(Strings.logChannel, ChannelType.Text).FirstOrDefault();
            return log;
        }

        public static Channel getLogChannel(MessageUpdatedEventArgs msg)
        {
            var log = msg.Server.FindChannels(Strings.logChannel, ChannelType.Text).FirstOrDefault();
            return log;
        }

        public static Channel getLogChannel(UserEventArgs usr)
        {
            var log = usr.Server.FindChannels(Strings.logChannel, ChannelType.Text).FirstOrDefault();
            return log;
        }
    }
}
