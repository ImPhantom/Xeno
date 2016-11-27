using System;
using System.Linq;
using Xeno.Utilities;

namespace Xeno.Modules
{
    class Events
    {
        public static void initEvents()
        {
            var client = Program.client;

            client.UserJoined += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":white_check_mark: **{e.User.Name}** has joined the {e.Server.Name} discord.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has joined {e.Server.Name}.");
            };

            client.UserLeft += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":x: **{e.User.Name}** has left the {e.Server.Name} discord.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has left {e.Server.Name}.");
            };

            client.UserBanned += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":x: **{e.User.Name}** has been banned.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has been banned from {e.Server.Name}");
            };

            client.UserUnbanned += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":x: **{e.User.Name}** has been unbanned.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has been Unbanned from {e.Server.Name}");
            };

            client.ChannelCreated += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":grey_exclamation: **{e.Channel.Name}** has been created.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.Channel.Name} has been created.");
            };

            client.ChannelDestroyed += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                await logChannel.SendMessage($":grey_exclamation: **{e.Channel.Name}** has been deleted.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.Channel.Name} has been deleted.");
            };

            client.MessageUpdated += async (s, e) =>
            {
                var before = e.Before.Text;
                var after = e.After.Text;
                var dif = "**Before:** " + before + "\n **After:** " + after;
                var logChannel = e.Server.FindChannels("serverlog").FirstOrDefault();
                if (before.Length > 75)
                {
                    await logChannel.SendMessage($":grey_exclamation: **{ e.User.Name} **edited their message. (too long)");
                }
                else
                {
                    await logChannel.SendMessage($":grey_exclamation: **{e.User.Name}** edited their message: \n " + dif);
                }
            };

            /*client.UserUpdated += async (s, e) => {

                var logChannel = e.Server.FindChannels("xenos").FirstOrDefault();
                if (e.After.VoiceChannel == null) return;

                if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

                await logChannel.SendMessage($":grey_exclamation: **{e.After.Name}** changed to the '*{e.After.VoiceChannel}*' channel.");
            };*/
        }
    }
}
