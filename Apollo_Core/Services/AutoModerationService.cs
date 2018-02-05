using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Services
{
    public partial class AutoModerationService
    {
        DiscordSocketClient _client;
        private DatabaseService _db;

        public Dictionary<ulong, UserJoinQueue> UserQueues { get; } = new Dictionary<ulong, UserJoinQueue>();

        public AutoModerationService(DiscordSocketClient client, DatabaseService db)
        {
            _db = db;
            _client = client;

            _client.UserJoined += UserJoined;
            _client.UserJoined += AntiRaid;
            _client.UserLeft += UserLeft;
            _client.MessageReceived += CacheMessage;
            _client.MessageUpdated += LogEditedMessage;
            _client.MessageDeleted += LogDeletedMessage;
            _client.MessageReceived += BlockInvites;
            _client.MessageReceived += BlockFilteredWords;
        }

    }
}
