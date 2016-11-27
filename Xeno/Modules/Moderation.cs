using Discord;
using Discord.Commands;
using System;
using System.Threading;
using Xeno.Utilities;


namespace Xeno.Modules
{
    class Moderation
    {
        public static void initModeration()
        {
            var client = Program.client;
            var commServ = client.GetService<CommandService>();

            #region ping
            commServ.CreateCommand("ping")
                .Description("Returns ping in milliseconds")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Calculating Ping...");
                    Thread.Sleep(1000);

                    Message[] msgToDelete;
                    msgToDelete = await e.Channel.DownloadMessages(1);
                    await e.Channel.DeleteMessages(msgToDelete);
                    msgToDelete = null;

                    await e.Channel.SendMessage("Pong!");
                    Thread.Sleep(5000);
                    Message[] msgToDelete1;
                    msgToDelete1 = await e.Channel.DownloadMessages(1);
                    await e.Channel.DeleteMessages(msgToDelete1);
                    msgToDelete1 = null;
                });
            #endregion

            #region cleanup
            commServ.CreateCommand("cleanup")
                .Description("Cleans up messages in chat (cleanup <int>)")
                .Parameter("amt", ParameterType.Required)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator == true)
                    {
                        Message[] messagesToDelete;
                        var amountToDelete = Convert.ToInt32($"{e.GetArg("amt")}");
                        messagesToDelete = await e.Channel.DownloadMessages(amountToDelete + 1);
                        await e.Channel.DeleteMessages(messagesToDelete);
                        messagesToDelete = null;
                        var cslAlert = "[" + Strings.appName + $"] {e.User.Name} has cleaned up chat.";
                        Console.WriteLine(cslAlert);
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Administrator** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion

            #region text
            commServ.CreateCommand("text")
                .Description("Creates a text channel in the server. (channel <name>)")
                .Parameter("name", ParameterType.Required)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.ManageChannels == true)
                    {
                        await e.Server.CreateChannel(e.GetArg("name"), ChannelType.Text);
                        await e.Channel.SendMessage($":grey_exclamation: You created the #{e.GetArg("name")} text channel.");
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Manage Roles** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion

            #region voice
            commServ.CreateCommand("voice")
                .Description("Creates a voice channel in the server. (channel <name>)")
                .Parameter("name", ParameterType.Required)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.ManageChannels == true)
                    {
                        await e.Server.CreateChannel(e.GetArg("name"), ChannelType.Voice);
                        await e.Channel.SendMessage($":grey_exclamation: You created the #{e.GetArg("name")} voice channel.");
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Manage Roles** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion
        }
    }
}
