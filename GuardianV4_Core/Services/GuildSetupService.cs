using Discord.WebSocket;
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
    }
}
