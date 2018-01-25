﻿using Discord.WebSocket;
using GuardianV4_Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Core.Extensions
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

    }
}
