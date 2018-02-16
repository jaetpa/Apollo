using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Services
{
    class StreamNotificationService
    {
        DiscordSocketClient _client;
        DatabaseService _db;
        public StreamNotificationService(DiscordSocketClient client, DatabaseService db)
        {
            _client = client;
            _db = db;

            _client.GuildMemberUpdated += StreamNotification;
        }

        private async Task StreamNotification(SocketUser arg1, SocketUser arg2)
        {
            if (arg2.Game?.StreamType == StreamType.NotStreaming)
            {
                return;
            }
            if ((arg1.Game?.StreamType == StreamType.NotStreaming)
                && (arg2.Game?.StreamType == StreamType.Twitch))
            {
                using (var uow = _db.UnitOfWork)
                {
                    var server = uow.Servers.Find((arg2 as SocketGuildUser).Guild.Id);
                    if (server == null)
                    {
                        return;
                    }
                    if (server.StreamTextChannelId.HasValue)
                    {
                        var channel = (arg2 as SocketGuildUser).Guild.GetTextChannel(server.StreamTextChannelId.Value);

                        var embed = new EmbedBuilder()
                            .WithEmbedType(EmbedType.Stream, arg2)
                            .WithDescription($"User **{arg2}** started streaming **{arg2.Game?.Name}** on Twitch!\n" +
                            $"{arg2.Game?.StreamUrl}")
                            .Build();

                        await channel.SendMessageAsync("", embed: embed);
                    }
                }
            }
        }
    }
}
