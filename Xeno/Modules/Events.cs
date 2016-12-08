using System;
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
                await Svr.getLogChannel(e).SendMessage($":white_check_mark: **{e.User.Name}** has joined the {e.Server.Name} discord.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has joined {e.Server.Name}.");
            };

            client.UserLeft += async (s, e) =>
            {
                await Svr.getLogChannel(e).SendMessage($":x: **{e.User.Name}** has left the {e.Server.Name} discord.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has left {e.Server.Name}.");
            };

            client.UserBanned += async (s, e) =>
            {
                await Svr.getLogChannel(e).SendMessage($":x: **{e.User.Name}** has been banned.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has been banned from {e.Server.Name}");
            };

            client.UserUnbanned += async (s, e) =>
            {
                await Svr.getLogChannel(e).SendMessage($":white_check_mark: **{e.User.Name}** has been unbanned.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.User.Name} has been Unbanned from {e.Server.Name}");
            };

            client.ChannelCreated += async (s, e) =>
            {
                await Svr.getLogChannel(e).SendMessage($":grey_exclamation: **{e.Channel.Name}** has been created.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.Channel.Name} has been created.");
            };

            client.ChannelDestroyed += async (s, e) =>
            {
                await Svr.getLogChannel(e).SendMessage($":grey_exclamation: **{e.Channel.Name}** has been deleted.");
                Console.WriteLine(Strings.infoEvent + $"({e.Server.Name}) {e.Channel.Name} has been deleted.");
            };

            client.MessageUpdated += async (s, e) =>
            {
                var before = Strings.replaceLinks(e.Before.RawText, Strings.getLinks(e.Before.RawText));
                var after = Strings.replaceLinks(e.After.RawText, Strings.getLinks(e.After.RawText));
                var dif = "**Before:** " + before + "\n **After:** " + after;

                // Bug: Messages without links in them are not posted. (too tired to fix)

                if (!e.User.IsBot)
                {
                    if (e.Before.Text.Length > 85)
                    {
                        await Svr.getLogChannel(e).SendMessage($":grey_exclamation: **{ e.User.Name} **edited their message. (*length*)");
                    }
                    else
                    {
                        await Svr.getLogChannel(e).SendMessage($":grey_exclamation: **{e.User.Name}** edited their message: \n " + dif);
                    }
                    
                }
            };
        }
    }
}
