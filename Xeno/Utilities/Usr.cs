﻿using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;

namespace Xeno.Utilities
{
    public class Usr
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

        public static string getUserInfo(User u, CommandEventArgs e, bool self)
        {
            if (self == false)
            {
                var info = new StringBuilder();
                info.AppendLine($@"**Name:** {u.Name}
**Nickname:** {u.Nickname}
**ID:** {u.Id}
**Joined On:** {u.JoinedAt}
**Current Game:** {u.CurrentGame.Value.Name}
**Role:** {u.Roles.FirstOrDefault()}
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
**Current Game:** {e.User.CurrentGame.Value.Name}
**Role:** {e.User.Roles.FirstOrDefault()}
**Avatar:** {e.User.AvatarUrl}");
                return pInfo.ToString();
            }
        }
    }
}
