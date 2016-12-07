using Discord.Commands;
using System.IO;
using System.Net.Http;

namespace Xeno.Modules
{
    class Settings
    {
        public static void initSettings()
        {
            var client = Program.client;
            var commServ = client.GetService<CommandService>();

            #region avatar
            commServ.CreateCommand("avatar")
                .Description("Sets the bot users avatar.")
                .Alias("botavatar", "setavatar")
                .Parameter("url", ParameterType.Required)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator == true)
                    {
                        var avatarUrl = $"{e.GetArg("url")}";
                        using (var http = new HttpClient())
                        {
                            using (var strResponse = await http.GetStreamAsync(avatarUrl))
                            {
                                var imgStream = new MemoryStream();
                                await strResponse.CopyToAsync(imgStream);
                                imgStream.Position = 0;

                                await client.CurrentUser.Edit(avatar: imgStream);
                            }
                        }
                        await e.Channel.SendMessage($":white_check_mark: You have set the bots avatar.");
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Administrator** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion

            #region status
            commServ.CreateCommand("status")
                .Description("Sets the bot users status tag.")
                .Alias("tag", "setstatus")
                .Parameter("tag", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator == true)
                    {
                        var statusTag = $"{e.GetArg("tag")}";
                        client.SetGame(statusTag);
                        await e.Channel.SendMessage($":white_check_mark: You have set the bots status.");
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
