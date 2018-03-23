using Discord;
using Discord.Commands;
using DiscordBot_Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DiscordBot_Core.Attributes;

namespace DiscordBot_Core.Modules.Moderation
{
    [RequireModeratorRole]
    public partial class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        private DatabaseService _db;
        private AutoModerationService _autoModService;

        public ModeratorModule(DatabaseService db, AutoModerationService autoModService)
        {
            _db = db;
            _autoModService = autoModService;
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
