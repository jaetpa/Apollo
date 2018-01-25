using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using GuardianV4_Core.Modules.Moderation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Services
{
    public class GuildSetupService
    {
        DiscordSocketClient _client;
        DatabaseService _db;
        public GuildSetupService(DiscordSocketClient client, DatabaseService db)
        {
            _client = client;
            _db = db;

            _client.GuildAvailable += EnsureGuildInDb;
            _client.GuildAvailable += SetLockdownModeTopic;
        }

        private Task EnsureGuildInDb(SocketGuild arg)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(arg.Id);

                if (entity == null)
                {
                    uow.Servers.Add(new GuardianV4_Data.Entities.ServerEntity { Id = arg.Id, GuildName = arg.Name });
                    uow.SaveChanges();
                }
                else if (entity.GuildName != arg.Name)
                {
                    uow.Servers.Update(entity);
                    entity.GuildName = arg.Name;
                    uow.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }

        private async Task SetLockdownModeTopic(SocketGuild arg)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(arg.Id);

                if (entity != null)
                {
                    await ModeratorModule.SetMainChannelTopic_Lockdown(entity, arg.GetMainChannel());
                }
            }
        }
    }
}
