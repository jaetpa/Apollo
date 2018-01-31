using Discord.WebSocket;
using Apollo_Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo_Core.Extensions
{
    public static class SocketGuildExtensions
    {
        public static SocketTextChannel GetMainChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.MainChannelId != null)
                {
                    return guild.GetTextChannel(entity.MainChannelId.Value);
                }
                else return null;
            }
        }

        public static SocketTextChannel GetWelcomeChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.WelcomeChannelId != null)
                {
                    return guild.GetTextChannel(entity.WelcomeChannelId.Value);
                }
                else return null;
            }
        }


        public static SocketTextChannel GetStaffChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.StaffChannelId != null)
                {
                    return guild.GetTextChannel(entity.StaffChannelId.Value);
                }
                else return null;
            }
        }
        public static SocketTextChannel GetLogChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.LogChannelId != null)
                {
                    return guild.GetTextChannel(entity.LogChannelId.Value);
                }
                else return null;
            }
        }
        public static SocketTextChannel GetDeleteLogChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.DeleteLogChannelId != null)
                {
                    return guild.GetTextChannel(entity.DeleteLogChannelId.Value);
                }
                else return null;
            }
        }
        public static SocketTextChannel GetBotChannel(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.BotChannelId != null)
                {
                    return guild.GetTextChannel(entity.BotChannelId.Value);
                }
                else return null;
            }
        }

        public static bool LockdownEnabled(this SocketGuild guild)
        {
            using (var uow = new DatabaseService().UnitOfWork)
            {
                var entity = uow.Servers.Find(guild.Id);
                if (entity?.Lockdown != null)
                {
                    return entity.Lockdown;
                }
                else return false;
            }
        }

    }
}
