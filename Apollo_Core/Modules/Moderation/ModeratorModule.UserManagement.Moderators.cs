using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot_Core.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("kick")]
        [Summary("Kicks a user from the server with an optional reason.")]
        [Remarks("!kick @florin_ro#9196")]
        public async Task KickUser(SocketGuildUser user, [Remainder] string reason = null)
        {
            await user.KickAsync(reason);

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Kick, user)
                .WithDescription($"User **{user}** was kicked from the server.\n" +
                $"Issuer: **{Context.User}**\n" +
                $"Reason: {reason ?? "none specified"}")
                .Build();

            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);

        }

        [Command("ban")]
        [Summary("Bans a user from the server with an optional reason.")]
        [Remarks("!ban @florin_ro#9196")]
        public async Task BanUser(SocketGuildUser user, [Remainder] string reason = null)
        {
            await Context.Guild.AddBanAsync(user, reason: reason);

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Ban, user)
                .WithDescription($"User **{user}** was banned from the server.\n" +
                $"Issuer: **{Context.User}**\n" +
                $"Reason: {reason ?? "none specified"}")
                .Build();

            await Context.Guild.GetMainChannel()?.SendMessageAsync($"**{user}** gets booed at and falls off stage.");
            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);

        }

        [Command("purgerecent")]
        [Summary("Bans a user from the server with an optional reason.")]
        [Remarks("!ban @florin_ro#9196")]
        //TODO: test purging
        public async Task PurgeRecent(int purgetime = 0, [Remainder] string banFlag = "")
        {
            var queue = await Context.Guild.GetUserJoinQueueAsync();
            if (queue == null)
            {
                await ReplyAsync($"There is no join queue to purge users from.");
                return;
            }

            int purgeCount = 0;

            foreach (var user in queue)
            {
                var guildUser = user as SocketGuildUser;
                
                if ((DateTimeOffset.Now - guildUser.JoinedAt) < TimeSpan.FromMinutes(purgetime))
                {
                    if (banFlag == "b" || banFlag == "B")
                    {
                        await Context.Guild.AddBanAsync(guildUser);
                    }
                    else
                    {
                        await guildUser.KickAsync($"Purged by {Context.User}");
                    }
                    purgeCount++;
                }
            }

            string banString = "";
            if (banFlag == "b" || banFlag == "B")
            {
                banString = "They were also banned.";
            }
            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.General, Context.User)
                .WithDescription($"{Context.User} purged **{purgeCount}** users that joined in the last {purgetime} minutes. {banString}")
                .Build();

            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);

        }

        [Command("clear")]
        [Summary("Deletes the last n messages in a channel, where n < 200")]
        [Remarks("!unmute @florin_ro#9196")]
        public async Task ClearMessages(int numToClear, [Remainder] string reason = null)
        {
            if (numToClear > 200)
            {
                await ReplyAsync("You can only clear 200 messages at a time.");
                return;
            }
            else if (numToClear < 1)
            {
                await ReplyAsync("You must clear at least 1 message.");
                return;
            }

            await Context.Message.DeleteAsync();
            var messages = await Context.Channel.GetMessagesAsync(numToClear).Flatten();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages.Reverse());

            await ReplyAsync($"**{numToClear}** messages were cleared (excluding the command).");

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.General, Context.User)
                .WithDescription($"User **{Context.User}** cleared **{numToClear}** messages in {(Context.Channel as SocketTextChannel).Mention}.")
                .Build();

            Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }

        [Command("rename")]
        [Summary("Changes a user's nickname.")]
        [Remarks("!rename @florin_ro#9196 scrub")]
        public async Task RenameUser(SocketGuildUser user, string newNickname)
        {
            await user.ModifyAsync(x => x.Nickname = new String(newNickname.Take(32).ToArray()));
        }



    }
}
