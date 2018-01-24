using Discord.WebSocket;
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

            _client.GuildAvailable += ConfigureGuildLogging;
        }

        private Task ConfigureGuildLogging(SocketGuild arg)
        {
            throw new NotImplementedException();
        }
    }
}
