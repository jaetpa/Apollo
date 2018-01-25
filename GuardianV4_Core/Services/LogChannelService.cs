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
            _client.UserUpdated += LogUsernameChanges;
            _client.GuildMemberUpdated += LogNicknameChanges;

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

        private async Task LogUsernameChanges(SocketUser arg1, SocketUser arg2)
        {
            if (arg1.Username == arg2.Username)
            {
                return;
            }
            else
            {
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.UsernameChange, arg2)
                    .WithDescription($"User **{arg1}** changed username to **{arg2}**")
                    .WithTimestamp()
                    .Build();
                await (arg2 as SocketGuildUser).Guild.GetLogChannel().SendMessageAsync("", embed: embed);
            }
        }

        private async Task LogNicknameChanges(SocketUser arg1, SocketUser arg2)
        {
            var user1 = arg1 as SocketGuildUser;
            var user2 = arg2 as SocketGuildUser;

            if (user1.Nickname == user2.Nickname)
            {
                return;
            }
            else
            {
                string message = String.Empty;
                if (user1.Nickname == null)
                {
                    message = $"User **{arg2}** set nickname **{user2.Nickname}#{arg2.Discriminator}**";
                }
                else if (user2.Nickname == null)
                {
                    message = $"User **{arg2}** removed nickname **{user1.Nickname}#{arg1.Discriminator}**";
                }
                else
                {
                    message = $"User **{arg2}** changed nickname from **{user1.Nickname}#{arg1.Discriminator}** to **{user2.Nickname}#{arg2.Discriminator}**";
                }
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.NicknameChange, arg2)
                    .WithDescription(message)
                    .WithTimestamp()
                    .Build();
                await (arg2 as SocketGuildUser).Guild.GetLogChannel().SendMessageAsync("", embed: embed);
            }
        }
    }
}
