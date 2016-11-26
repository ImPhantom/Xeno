using Discord.Commands;
using System.IO;
using System.Net.Http;

namespace Xeno
{
    class Settings
    {
        public static void initSettings()
        {
            var client = Program.client;
            var commServ = client.GetService<CommandService>();

            #region setavatar
            commServ.CreateCommand("avatar")
                .Description("Sets the bot users avatar.")
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
                        await e.Channel.SendMessage($"You have set the bots avatar.");
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Administrator** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion

            #region setgame
            commServ.CreateCommand("status")
                .Description("Sets the bot users status tag.")
                .Parameter("tag", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator == true)
                    {
                        var statusTag = $"{e.GetArg("tag")}";
                        client.SetGame(statusTag);
                        await e.Channel.SendMessage($"You have set the bots status.");
                    }
                    else
                    {
                        var permErrorMessage = $"{e.User.Mention} you must have **Administrator** permissions to run that command.";
                        await e.Channel.SendMessage(permErrorMessage);
                    }
                });
            #endregion

            #region debuf
            commServ.CreateCommand("debug")
                .Description("Sets the bots logging mode to debug (Only stays for session.)")
                .Parameter("bool", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator == true)
                    {
                        if (e.GetArg("bool") == "true")
                        {
                            Program.debug = true;
                            await e.Channel.SendMessage($":grey_exclamation: You set the debug mode to true!");
                        } else if(e.GetArg("bool") == "false")
                        {
                            Program.debug = false;
                            await e.Channel.SendMessage($":grey_exclamation: You set the debug mode to false!");
                        }
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
