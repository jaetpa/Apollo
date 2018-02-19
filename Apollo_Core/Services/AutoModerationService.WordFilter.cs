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

        private async Task BlockFilteredWords(SocketMessage arg)
        {
            if (arg.Author is SocketGuildUser user)
            {

                if (arg.Content.ToUpper() == "NIG"
                    || arg.Content.ToUpper().Contains(" NIG ")
                    || arg.Content.ToUpper().Contains("NIGGA")
                    || arg.Content.ToUpper().Contains("NIGGER"))
                {
                    try
                    {
                        if (arg.Content.ToUpper().Contains("SNIGGER")
                            || arg.Content.ToUpper().Contains("NIGGARD"))
                        {
                            return;
                        }
                        await arg.DeleteAsync();

                        if (user.Roles.Any(x => x.Name.ToUpper() == "FUTURE FRIENDOS"))
                        {
                            await user.Guild.AddBanAsync(user, 0, "Using banned words");
                        }
                        else
                        {
                            return;
                        }
                    }
                    finally
                    {
                        var embed = new EmbedBuilder()
                            .WithEmbedType(DiscordBot_Core.EmbedType.Ban, user)
                            .WithDescription($"User **{user}** was banned for using banned words.")
                            .Build();
                        user.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
                    }
                }
            }
        }

    }
}
