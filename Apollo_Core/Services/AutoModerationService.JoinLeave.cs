using Discord;
using Discord.WebSocket;
using DiscordBot_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Services
{
    public partial class AutoModerationService
    {
        public List<ulong> KickedUsers { get; set; } = new List<ulong>();
        public List<ulong> BannedUsers { get; set; } = new List<ulong>();
        public Dictionary<ulong, ulong> JoinMessages { get; set; } = new Dictionary<ulong, ulong>();

        private async Task AntiRaid(SocketGuildUser arg)
        {
            int recentJoins = 0;
            List<IUser> recentJoinUsers = new List<IUser>();
            foreach (var user in UserQueues[arg.Id].Users)
            {
                if ((DateTimeOffset.Now - (user as SocketGuildUser).JoinedAt) < TimeSpan.FromSeconds(20))
                {
                    recentJoins++;
                    recentJoinUsers.Add(user);
                }
            }

            if (recentJoins >= 3)
            {
                var mutedRole = await arg.Guild.GetOrCreateMutedRole();

                foreach (var user in recentJoinUsers)
                {
                    await (user as SocketGuildUser).AddRoleAsync(mutedRole);
                }

                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.General, _client.CurrentUser)
                    .WithDescription($"Anti-raid automatically muted {recentJoins} users that joined in quick succession.")
                    .Build();

                await arg.Guild.GetLogChannel().SendMessageAsync("", embed: embed);
            }
        }

        private async Task UserJoined(SocketGuildUser arg)
        {
            KickedUsers.RemoveAll(x => x == arg.Id);
            BannedUsers.RemoveAll(x => x == arg.Id);

            var userQueue = GetOrCreateUserQueue(arg.Guild);
            userQueue.Enqueue(arg);

            var welcomeChannel = arg.Guild.GetWelcomeChannel();
            var logChannel = arg.Guild.GetLogChannel();

            if (arg.Guild.LockdownEnabled())
            {
                BlockUserJoin(arg);
                return;
            }

            if (welcomeChannel != null)
            {
                var msg = await welcomeChannel.SendMessageAsync($"**{arg.Mention}** gets on stage and takes the mic.");

                JoinMessages.TryAdd(arg.Id, msg.Id);

                //TODO: Add time since account creation
                //TODO: Add join card
            }
            if (logChannel != null)
            {
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.Join, arg)
                    .WithDescription($"User **{arg}** joined the server. Account created **{arg.CreatedAt:ddd dd MMM, yyyy} ({GetTimeSince(arg.CreatedAt)} ago).**")
                    .Build();
                await logChannel.SendMessageAsync("", embed: embed);
            }
        }

        private UserJoinQueue GetOrCreateUserQueue(IGuild guild)
        {
            if (UserQueues.ContainsKey(guild.Id))
            {
                return UserQueues[guild.Id];
            }
            else
            {
                UserQueues.Add(guild.Id, new UserJoinQueue());
                return UserQueues[guild.Id];
            }
        }

        private void BlockUserJoin(SocketGuildUser user)
        {
            user.KickAsync("Joined during Lockdown mode.");
            KickedUsers.Add(user.Id);
            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.LockdownKick, user)
                .WithDescription($"User **{user}** was automatically kicked by Lockdown mode. Account created {(int)(DateTimeOffset.Now - user.CreatedAt).TotalDays} days ago.")
                .Build();

            user.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }

        private async Task UserLeft(SocketGuildUser arg)
        {
            try
            {
                if (JoinMessages.ContainsKey(arg.Id))
                {
                    var msg = await arg.Guild.GetMainChannel().GetMessageAsync(JoinMessages[arg.Id]);
                    await (msg as SocketUserMessage).ModifyAsync(x => x.Content = msg.Content + ".. then drops it and leaves.");

                    JoinMessages.Remove(arg.Id);
                }
                if (KickedUsers.Contains(arg.Id) || BannedUsers.Contains(arg.Id))
                {
                    return;
                }

                //TODO: Stop leave messages for people kicked during lockdown

                using (var uow = _db.UnitOfWork)
                {
                    var server = uow.Servers.Find(arg.Guild.Id);
                    if (server == null)
                    {
                        return;
                    }
                    if (arg.Guild.LockdownEnabled() && arg.JoinedAt > server.LockdownTime)
                    {
                        return;
                    }
                }

                var logChannel = arg.Guild.GetLogChannel();
                if (logChannel != null)
                {
                    var embed = new EmbedBuilder()
                        .WithEmbedType(EmbedType.Leave, arg)
                        .WithDescription($"User **{arg}** left the server after {GetTimeSince(arg.JoinedAt)}.")
                        .Build();
                    await logChannel.SendMessageAsync("", embed: embed);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        private async Task UserBanned(SocketUser arg1, SocketGuild arg2)
        {
            if (BannedUsers.Contains(arg1.Id))
            {
                return;
            }
            else
            {
                BannedUsers.Add(arg1.Id);
            }
            var logChannel = arg2.GetLogChannel();
            if (logChannel != null)
            {
                var embed = new EmbedBuilder()
                    .WithEmbedType(EmbedType.Ban, arg1)
                    .WithDescription($"User **{arg1}** was banned from the server.")
                    .Build();

                await logChannel.SendMessageAsync("", embed: embed);
            }
        }

        private string GetTimeSince(DateTimeOffset? eventTime)
        {
            if (!eventTime.HasValue)
            {
                return "...some time";
            }
            TimeSpan duration = (DateTimeOffset.Now - eventTime).Value;

            if (duration < TimeSpan.FromMinutes(1))
            {
                return $"{duration.Seconds} seconds";
            }
            else if (duration < TimeSpan.FromHours(1))
            {
                return $"{duration.Minutes} minutes, {duration.Seconds} seconds";
            }
            else if (duration < TimeSpan.FromDays(1))
            {
                return $"{duration.Hours} hours, {duration.Minutes} minutes";
            }
            else
            {
                return $"{duration.Days} days";
            }
        }
    }
}

