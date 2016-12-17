using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using System.IO;
using System.Net.Http;
using Xeno.Utilities;

namespace Xeno.Modules
{
    public class Settings : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            //var client = Program.client;
            //var commServ = client.GetService<CommandService>();
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands("", commServ =>
            {
                #region set
                commServ.CreateCommand("settings")
                    .Description("Shows help for the set commands.")
                    .Do(async (e) =>
                    {
                        await e.Channel.SendMessage(Strings.setHelp);
                    });
                #endregion

                #region set (cmd group)
                commServ.CreateGroup("set", (cgb) =>
                {
                    #region avatar
                    cgb.CreateCommand("avatar")
                        .Description("Sets the bot users avatar.")
                        .Alias("botavatar", "setavatar")
                        .MinPermissions(5)
                        .Parameter("url", ParameterType.Required)
                        .Do(async (e) =>
                        {
                            var avatarUrl = $"{e.GetArg("url")}";
                            using (var http = new HttpClient())
                            {
                                using (var strResponse = await http.GetStreamAsync(avatarUrl))
                                {
                                    var imgStream = new MemoryStream();
                                    await strResponse.CopyToAsync(imgStream);
                                    imgStream.Position = 0;

                                    await _client.CurrentUser.Edit(avatar: imgStream);
                                    await e.Channel.SendMessage($":white_check_mark: You have set the bots avatar.");
                                }
                            }
                        });
                    #endregion

                    #region status
                    cgb.CreateCommand("status")
                        .Description("Sets the bot users status tag.")
                        .MinPermissions(4)
                        .Parameter("tag", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            var statusTag = $"{ e.GetArg("tag")}";
                            _client.SetGame(statusTag);
                            await e.Channel.SendMessage($":white_check_mark: You have set the bots status.");
                        });
                    #endregion

                    #region nick
                    cgb.CreateCommand("nick")
                        .Description("Sets the bots description.")
                        .MinPermissions(5)
                        .Parameter("string", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            var nick = $"{e.GetArg("string")}";
                            await e.Server.CurrentUser.Edit(nickname: nick);
                            await e.Channel.SendMessage(":white_check_mark: You have set the bots nickname!");
                        });
                    #endregion

                    #region username
                    cgb.CreateCommand("username")
                        .Description("Sets the bots username. (limited)")
                        .MinPermissions(5)
                        .Parameter("string", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            var username = $"{e.GetArg("string")}";
                            await _client.CurrentUser.Edit(username: username);
                            await e.Channel.SendMessage(":white_check_mark: You have set the bots username!");
                        });
                    #endregion
                    #endregion
                });
            });
        }
    }
}
