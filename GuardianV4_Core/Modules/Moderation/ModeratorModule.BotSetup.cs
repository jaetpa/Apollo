using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("setmainchannel")]
        [Summary("Designates a text channel as the server's main/general channel.")]
        [Remarks("!setmainchannel #general")]
        public async Task SetMainChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.MainChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **main** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setwelcomechannel")]
        [Summary("Designates a text channel as the server's welcoming channel (where join notifications will be posted publicly).")]
        [Remarks("!setwelcomechannel #general")]
        public async Task SetWelcomeChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.WelcomeChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **welcome** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setstaffchannel")]
        [Summary("Designates a text channel as the server's staff channel.")]
        [Remarks("!setstaffchannel #staff")]
        public async Task SetStaffChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.StaffChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **staff** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }


        [Command("setlogchannel")]
        [Summary("Designates a text channel as the server's moderation logging channel.")]
        [Remarks("!setlogchannel #log")]
        public async Task SetLogChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.LogChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **log** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setdeletelogchannel")]
        [Summary("Designates a text channel as the server's deleted message logging channel.")]
        [Remarks("!setdeletelogchannel #delete-log")]
        public async Task SetDeleteLogChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.DeleteLogChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **delete log** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setbotchannel")]
        [Summary("Designates a text channel as the server's bot configuration channel.")]
        [Remarks("!setbotchannel #bots")]
        public async Task SetBotChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.BotChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **bot configuration** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setvoicetextchannel")]
        [Summary("Designates a text channel as the server's voice text channel.")]
        [Remarks("!setvoicetextchannel #vc")]
        public async Task SetVoiceTextChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.VoiceTextChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **voice text** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

        [Command("setstreamchannel")]
        [Summary("Designates a text channel as the server's stream notification channel.")]
        [Remarks("!setvoicetextchannel #vc")]
        public async Task SetStreamNotificationChannel(SocketTextChannel channel = null)
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);
                if (entity != null)
                {
                    uow.Servers.Update(entity);
                    entity.StreamTextChannelId = channel?.Id ?? Context.Channel.Id;
                    uow.SaveChanges();
                    await ReplyAsync($"Channel {channel?.Mention ?? (Context.Channel as SocketTextChannel).Mention} has been set as the **stream notification** channel.");
                }
                else
                {
                    await ReplyAsync("This server was not found in the database.");
                }
            }
        }

    }
}
