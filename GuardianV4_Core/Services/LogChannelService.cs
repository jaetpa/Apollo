using Discord;
using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Services
{
    class LogChannelService
    {
        DiscordSocketClient _client;
        public LogChannelService(DiscordSocketClient client)
        {
            _client = client;

            _client.GuildAvailable += LogConnected;
        }

        private async Task LogConnected(SocketGuild arg)
        {
            var logChannel = arg.GetLogChannel();

            if (logChannel == null)
                return;

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Connect, _client.CurrentUser)
                .Build();
            await logChannel.SendMessageAsync("", embed: embed);
        }
    }
}
