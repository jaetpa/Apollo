using Discord;
using Discord.Commands;
using GuardianV4_Core.Extensions;
using GuardianV4_Core.Services;
using GuardianV4_Repository.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        private DatabaseService _db;

        public ModeratorModule(DatabaseService db)
        {
            _db = db;
        }

        [Command("test")]
        public async Task Test()
        {
            var embedBuilder = new EmbedBuilder().WithEmbedType(EmbedType.UsernameChange, Context.User).WithTimestamp();
            var embed = embedBuilder.Build();
            await ReplyAsync("", embed: embed);
        }

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
    }
}
