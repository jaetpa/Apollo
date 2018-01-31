using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using GuardianV4_Core.Services;
using Newtonsoft.Json;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("addcommand")]
        public async Task AddCommand(string commandName, string reply)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.CustomCommands.Find(x => x.Command == commandName && x.GuildId == Context.Guild.Id).FirstOrDefault();
                if (entity != null)
                {
                    await ReplyAsync($"A command with the name **{commandName}** already exists.");
                    return;
                }
                else
                {
                    uow.CustomCommands.Add(new GuardianV4_Data.Entities.CustomCommandEntity
                    {
                        Command = commandName,
                        Reply = reply,
                        GuildId = Context.Guild.Id
                    });
                    uow.SaveChanges();
                    await ReplyAsync($"Command **{commandName}** has been added.");

                }
            }
        }

        [Command("removecommand")]
        public async Task RemoveCommand(string commandName)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.CustomCommands.Find(x => x.Command == commandName && x.GuildId == Context.Guild.Id).FirstOrDefault();
                if (entity == null)
                {
                    await ReplyAsync($"A command with the name **{commandName}** does not exist.");
                    return;
                }
                else
                {
                    uow.CustomCommands.Remove(entity);
                    uow.SaveChanges();
                    await ReplyAsync($" Command **{commandName}** has been removed.");

                }
            }
        }

        [Command("updatecommand")]
        public async Task RemoveCommand(string commandName, string reply)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.CustomCommands.Find(x => x.Command == commandName && x.GuildId == Context.Guild.Id).FirstOrDefault();
                if (entity == null)
                {
                    await ReplyAsync($"A command with the name **{commandName}** does not exist.");
                    return;
                }
                else
                {
                    entity.Reply = reply;
                    uow.CustomCommands.Update(entity);
                    uow.SaveChanges();
                    await ReplyAsync($"Command **{commandName}** has been updated.");

                }
            }
        }

    }
}
