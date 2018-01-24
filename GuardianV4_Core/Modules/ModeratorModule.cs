using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules
{
    public class ModeratorModule:ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task Test()
        {
            var embedBuilder = new EmbedMessageBuilder().WithEmbedType(EmbedType.UsernameChange).WithTimestamp();
            var embed = embedBuilder.Build();
            await ReplyAsync("", embed: embed);
        }
    }
}
