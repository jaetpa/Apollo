using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Core.Services
{
    public partial class AutoModerationService
    {
        DiscordSocketClient _client;
        public Dictionary<ulong, UserJoinQueue> UserQueues { get; } = new Dictionary<ulong, UserJoinQueue>();

        public AutoModerationService(DiscordSocketClient client)
        {
            _client = client;

            _client.UserJoined += UserJoined;
            _client.UserJoined += AntiRaid;
            _client.UserLeft += UserLeft;
        }
    }
}
