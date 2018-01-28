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
        Dictionary<ulong, UserJoinQueue> _userQueues = new Dictionary<ulong, UserJoinQueue>();

        public AutoModerationService(DiscordSocketClient client)
        {
            _client = client;

            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;

        }

        private async Task UserJoined(SocketGuildUser arg)
        {
            var userQueue = GetOrCreateUserQueue(arg);
            var welcomeChannel = arg.Guild.GetWelcomeChannel();
            var logChannel = arg.Guild.GetLogChannel();

            if (arg.Guild.LockdownEnabled())
            {
                BlockUserJoin(arg);
                return;
            }

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

        private UserJoinQueue GetOrCreateUserQueue(IGuild guild)
        {
            if (_userQueues.ContainsKey(guild.Id))
            {
                return _userQueues[guild.Id];
            }
            else
            {
                _userQueues.Add(guild.Id, new UserJoinQueue());
                return _userQueues[guild.Id];
            }
        }

        private void BlockUserJoin(SocketGuildUser user)
        {
            user.KickAsync("Joined during Lockdown mode.");
            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.LockdownKick, user)
                .WithDescription($"User **{user}** was automatically kicked by Lockdown mode.")
                .Build();

            user.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }

        private async Task UserLeft(SocketGuildUser arg)
        {
            //TODO: Stop leave messages for people kicked during lockdown
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
