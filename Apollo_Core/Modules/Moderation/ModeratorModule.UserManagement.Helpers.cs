using DiscordBot_Core.Attributes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Modules.Moderation
{
    [RequireHelperRole]
    public class HelperModule : ModuleBase<SocketCommandContext>
    {
        [Command("mute")]
        [Summary("Mutes a user. They will not be able to speak in text or voice channels.")]
        [Remarks("!mute @florin_ro#9196")]
        public async Task MuteUser(SocketGuildUser user, [Remainder] string reason = null)
        {
            var mutedRole = await Context.Guild.GetOrCreateMutedRole();

            if (user.Roles.Any(role => role.Name.ToUpper() == "CONDUCTORS"))
            {
                await ReplyAsync($"You cannot use this command on a member with this role.");
                return;
            }

            if (user.Roles.Any(role => role.Id == mutedRole.Id))
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
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }

        [Command("unmute")]
        [Summary("Unmutes a user. They will be able to speak again.")]
        [Remarks("!unmute @florin_ro#9196")]
        public async Task UnmuteUser(SocketGuildUser user)
        {
            var mutedRole = await Context.Guild.GetOrCreateMutedRole();
            if (!user.Roles.Any(role => role.Id == mutedRole.Id))
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
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
        }

        [Command("tmute", RunMode = RunMode.Async)]
        [Summary("Mutes a user for a specified amount of time in minutes. Set to 5 minutes by default.")]
        [Remarks("!unmute @florin_ro#9196")]
        public async Task TimedMute(SocketGuildUser user, int muteTime = 5, [Remainder] string reason = null)
        {
            var mutedRole = await Context.Guild.GetOrCreateMutedRole();

            if (user.Roles.Any(role => role.Name.ToUpper() == "CONDUCTORS"))
            {
                await ReplyAsync($"You cannot use this command on a member with this role.");
                return;
            }

            if (user.Roles.Any(role => role.Id == mutedRole.Id))
            {
                await ReplyAsync($"**{user}** is already muted.");
                return;
            }
            string msg = "";

            if (muteTime < 1)
            {
                await ReplyAsync("You must mute for at least 1 minute.");
                return;
            }
            else if (muteTime > 5000)
            {
                muteTime = 5000;
                msg = "Oh come on, 5000 minutes is enough.";
            }

            await user.AddRoleAsync(mutedRole);

            var muteEmbed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Mute, user)
                .WithDescription($"User **{user}** was muted for **{muteTime}** minutes.\n" +
                    $"Issuer: **{Context.User}**\n" +
                    $"Reason: {reason ?? "none specified"}")
                .Build();

            await ReplyAsync(msg, embed: muteEmbed);
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: muteEmbed);

            await Task.Delay(TimeSpan.FromMinutes(muteTime));

            await user.RemoveRoleAsync(mutedRole);

            var unmuteEmbed = new EmbedBuilder()
                .WithEmbedType(EmbedType.Unmute, user)
                .WithDescription($"User **{user}** was unmuted after **{muteTime}** minutes.\n")
                .Build();

            await ReplyAsync("", embed: unmuteEmbed);
            await Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: unmuteEmbed);

        }

    }
}
