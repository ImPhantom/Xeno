using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xeno.Utilities
{
    class Usr
    {
        public static User getUser(CommandEventArgs e)
        {
            ulong id;
            User u = null;
            string user = e.Args[0];
            if (!string.IsNullOrWhiteSpace(user))
            {
                if (e.Message.MentionedUsers.Count() == 1)
                    u = e.Message.MentionedUsers.FirstOrDefault();
                else if (e.Server.FindUsers(user).Any())
                    u = e.Server.FindUsers(user).FirstOrDefault();
                else if (ulong.TryParse(user, out id))
                    u = e.Server.GetUser(id);
            }
            return u;
        }

        public static string getUserInfo(User u, CommandEventArgs e, bool bl)
        {
            if (bl == false)
            {
                var info = new StringBuilder();
                info.AppendLine($@"**Name:** {u.Name}
**Nickname:** {u.Nickname}
**ID:** {u.Id}
**Joined On:** {u.JoinedAt}
**Avatar:** {u.AvatarUrl}");
                return info.ToString();
            }
            else
            {
                var pInfo = new StringBuilder();
                pInfo.AppendLine($@"**Name:** {e.User.Name}
**Nickname:** {e.User.Nickname}
**ID:** {e.User.Id}
**Joined On:** {e.User.JoinedAt}
**Avatar:** {e.User.AvatarUrl}");
                return pInfo.ToString();
            }
        }
    }
}
