using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
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
            await Context.Guild.GetLogChannel().SendMessageAsync("", embed: embed);

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

            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel().SendMessageAsync("", embed: embed);

        }

        [Command("mute")]
        [Summary("Mutes a user. They will not be able to speak in text or voice channels.")]
        [Remarks("!mute @florin_ro#9196")]
        public async Task MuteUser(SocketGuildUser user, [Remainder] string reason = null)
        {
            var mutedRole = await Context.Guild.GetOrCreateMutedRole();

            if (user.Roles.FirstOrDefault(role => role.Id == mutedRole.Id) != null)
            {
                await ReplyAsync($"**{user}** is already muted.");
                return;
            }

            await user.AddRoleAsync(mutedRole);

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Mute, user)
                .WithDescription($"User **{user}** was muted.\n" +
                    $"Issuer: **{Context.User}**\n" +
                    $"Reason: {reason ?? "none specified"}")
                .Build();

            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel().SendMessageAsync("", embed: embed);
        }

        [Command("unmute")]
        [Summary("Unmutes a user. They will be able to speak again.")]
        [Remarks("!unmute @florin_ro#9196")]
        public async Task UnmuteUser(SocketGuildUser user)
        {
            var mutedRole = await Context.Guild.GetOrCreateMutedRole();
            if (user.Roles.FirstOrDefault(role => role.Id == mutedRole.Id) == null)
            {
                await ReplyAsync($"**{user}** is not muted.");
                return;
            }

            await user.RemoveRoleAsync(mutedRole);

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Unmute, user)
                .WithDescription($"User **{user}** was unmuted.\n" +
                    $"Issuer: **{Context.User}**\n")
                .Build();

            await ReplyAsync("", embed: embed);
            await Context.Guild.GetLogChannel().SendMessageAsync("", embed: embed);
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
            var messages = await Context.Channel.GetMessagesAsync(numToClear).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages.Reverse());

            await ReplyAsync($"**{numToClear}** messages were cleared (excluding the command).");

            var embed = new EmbedBuilder()
                .WithEmbedType(EmbedType.General, Context.User)
                .WithDescription($"User **{Context.User}** cleared **{numToClear}** messages in {(Context.Channel as SocketTextChannel).Mention}.")
                .Build();

            Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }


    }
}
