using Discord;
using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Services
{
    public partial class AutoModerationService
    {
        DiscordSocketClient _client;

        public AutoModerationService(DiscordSocketClient client)
        {
            _client = client;

            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;

        }

        private async Task UserJoined(SocketGuildUser arg)
        {
            var welcomeChannel = arg.Guild.GetWelcomeChannel();
            var logChannel = arg.Guild.GetLogChannel();

            if (welcomeChannel != null)
            {
                await welcomeChannel.SendMessageAsync($"User **{arg.Mention}** joined the server.");
                //TODO: Add time since account creation
                //TODO: Add join card
            }
            if (logChannel != null)
            {
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.Join, arg)
                    .WithDescription($"User **{arg}** joined the server.")
                    .Build();
                await logChannel.SendMessageAsync("", embed: embed);
            }
        }

        private async Task UserLeft(SocketGuildUser arg)
        {
            var welcomeChannel = arg.Guild.GetWelcomeChannel();
            var logChannel = arg.Guild.GetLogChannel();

            if (welcomeChannel != null)
            {
                await welcomeChannel.SendMessageAsync($"User **{arg.Mention}** left the server.");
                //TODO: Add time since join
            }
            if (logChannel != null)
            {
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.Leave, arg)
                    .WithDescription($"User **{arg}** left the server.")
                    .Build();
                await logChannel.SendMessageAsync("", embed: embed);

            }
        }
    }
}
