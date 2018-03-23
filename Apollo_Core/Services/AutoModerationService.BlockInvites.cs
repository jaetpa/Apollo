using Discord;
using Discord.WebSocket;
using DiscordBot_Core;
using DiscordBot_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Services
{
    public partial class AutoModerationService
    {
        private async Task BlockInvites(SocketMessage arg)
        {
            if (arg.Author is SocketGuildUser user)
            {
                List<string> guildInviteCodes = (await user.Guild.GetInvitesAsync()).Select(x => x.Code).ToList();
                List<string> serverLists = new List<string> { "discord.gg/", "discord.me/", "discordservers.com/", "discordlist.me/", "discord.shoutwiki.com/", "discordlist.com/", "discordlist.net/" };

                foreach (var inviteCode in guildInviteCodes)
                {
                    if (arg.Content.Contains(inviteCode))
                    {
                        return;
                    }
                }
                foreach (var listUrl in serverLists)
                {
                    if (arg.Content.Contains(listUrl))
                    {
                        await arg.DeleteAsync();

                        if (user.Roles.Any(x => x.Name.ToUpper() == "FUTURE FRIENDOS"))
                        {
                            try
                            {
                                await user.Guild.AddBanAsync(user, 0, "Posting invite link");
                                BannedUsers.Add(user.Id);
                            }
                            finally
                            {
                                var embed = new EmbedBuilder()
                                    .WithEmbedType(DiscordBot_Core.EmbedType.Ban, user)
                                    .WithDescription($"User **{user}** was banned for posting an invite link.")
                                    .Build();
                                user.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
                            }
                        }
                    }
                }
            }
        }
    }
}
