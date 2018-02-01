using Discord.Commands;
using Discord.WebSocket;
using Apollo_Core.Extensions;
using Apollo_Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apollo_Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    class RequireModeratorRoleAttribute : PreconditionAttribute
    {
        private DatabaseService _db;

        //public RequireModeratorRoleAttribute(DatabaseService db)
        //{
        //    _db = db;
        //}

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.User is SocketGuildUser user && user.GuildPermissions.BanMembers)
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError("You do not have the required role."));
            }
        }

        //public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        //{
        //    if ((context.User as SocketGuildUser).IsMod(_db))
        //    {
        //        return Task.FromResult(PreconditionResult.FromSuccess());
        //    }
        //    else return Task.FromResult(PreconditionResult.FromError("You do not have the required role."));

        //}
    }
}
