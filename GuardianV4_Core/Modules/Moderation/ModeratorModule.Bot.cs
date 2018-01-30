using Discord.Commands;
using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using System;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("setgame")]
        [Summary("Sets Guardian's game status.")]
        [Remarks("!setgame Cleaning tables")]
        public async Task SetGame([Remainder] string text = "")
        {
            await Context.Client.SetGameAsync(text);
        }

        [Command("c")]
        [Summary("Posts a message to cafe via Guardian.")]
        [Remarks("!c I'm always watching")]
        public async Task SendMessageToChannel([Remainder] string text = "")
        {
            var channel = Context.Guild.GetMainChannel();
            await channel.SendMessageAsync(text);
        }

        [Command("say")]
        [Summary("Posts a message to a specified channel via Guardian.")]
        [Remarks("!say #general I'm always watching")]
        public async Task SendMessageToChannel(SocketTextChannel channel = null, [Remainder] string text = "")
        {
            if (channel == null)
            {
                channel = Context.Channel as SocketTextChannel;
            }
            await channel.SendMessageAsync(text);
        }

        [Command("bump", RunMode = RunMode.Async)]
        [Summary("Reminds the owner to bump the server in 6 hours.")]
        [Remarks("!bump")]
        public async Task BumpReminder([Remainder] string text = "")
        {
            if (Context.User.Id != Context.Guild.OwnerId)
            {
                await ReplyAsync("Only the owner can use this command.");
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await new TaskFactory().StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromHours(6));
                await dmChannel.SendMessageAsync("It's time to bump the server! :alarm_clock:");
            });
        }

    }
}
