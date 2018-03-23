using Discord.WebSocket;
using DiscordBot_Core.Extensions;
using DiscordBot_Core.Modules.Moderation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Services
{
    public class GuildSetupService
    {
        DiscordSocketClient _client;
        DatabaseService _db;
        public GuildSetupService(DiscordSocketClient client, DatabaseService db)
        {
            _client = client;
            _db = db;

            _client.GuildAvailable += EnsureGuildInDbAsync;
            _client.GuildAvailable += SetLockdownModeTopic;
        }

        private async Task EnsureGuildInDbAsync(SocketGuild arg)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(arg.Id);

                if (entity == null)
                {
                    uow.Servers.Add(new DiscordBot_Data.Entities.ServerEntity { Id = arg.Id, GuildName = arg.Name });
                    uow.SaveChanges();
                }
                else if (entity.GuildName != arg.Name)
                {
                    entity.GuildName = arg.Name;
                    uow.Servers.Update(entity);
                    uow.SaveChanges();
                }
                else
                {
                    var dmChannel = await arg.Owner.GetOrCreateDMChannelAsync();

                    var timeUntilDue = TimeSpan.FromHours(6) - (DateTimeOffset.Now - entity.LastBumpTime);

                    if (timeUntilDue >= TimeSpan.Zero)
                    {
                        await new TaskFactory().StartNew(async () =>
                        {
                            await Task.Delay(timeUntilDue);
                            await dmChannel.SendMessageAsync("It's time to bump the server! :alarm_clock:");
                        });
                    }

                }
            }
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
