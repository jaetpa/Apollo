using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Services
{
    class CustomCommandService
    {
        private DiscordSocketClient _client;
        private DatabaseService _db;

        public CustomCommandService(DiscordSocketClient client, DatabaseService db)
        {
            _client = client;
            _db = db;

            _client.MessageReceived += RunCustomCommand;
        }

        private async Task RunCustomCommand(SocketMessage arg)
        {
            if (!(arg.Channel is SocketTextChannel channel))
            {
                return;
            }
            if (!arg.Content.StartsWith("!"))
            {
                return;
            }
            else
            {
                var commandName = arg.Content.Split("!")?[1];
                using (var uow = _db.UnitOfWork)
                {
                    var entity = uow.CustomCommands.Find(x => x.Command == commandName && x.GuildId == channel.Guild.Id).FirstOrDefault();

                    if (entity == null)
                    {
                        return;
                    }

                    await channel.SendMessageAsync(entity.Reply);
                }

            }
        }
    }
}
