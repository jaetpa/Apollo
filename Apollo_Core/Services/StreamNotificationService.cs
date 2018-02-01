using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apollo_Core.Services
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
            if (arg2.Activity == null)
            {
                return;
            }
            if (arg1.Activity?.Type != ActivityType.Streaming
                && arg2.Activity.Type == ActivityType.Streaming)
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
                            .WithDescription($"User **{arg2}** started streaming **{arg2.Activity.Name}** on Twitch!")
                            .Build();

                        await channel.SendMessageAsync("", embed: embed);
                    }
                }
            }
        }
    }
}
