using Discord;
using Discord.Commands;
using GuardianV4_Core.Services;
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
    }
}
