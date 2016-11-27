using Discord;
using Discord.Commands;
using System;
using System.Threading;

namespace Xeno
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
                .Description("Cleans up messages in chat (~cleanup <int>)")
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
                        var cslAlert = $"[Xenos] {e.User.Name} has cleaned up chat.";
                        Console.WriteLine(cslAlert);
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Administrator** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion
        }
    }
}
