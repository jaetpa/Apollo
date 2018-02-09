using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Attributes
{
    class RequireHelperRoleAttribute : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.User is SocketGuildUser user)
            {
                if (user.Roles.Any(x => x.Name.ToUpper() == "HELPERS")
                    || user.Roles.Any(x => x.Name.ToUpper() == "CONDUCTORS"))
                {
                    return PreconditionResult.FromSuccess();
                }
                else
                {
                    return PreconditionResult.FromError("You do not have the required role.");
                }
            }
            else
            {
                return PreconditionResult.FromError("This command must be executed in a guild.");
            }
        }
    }
}
