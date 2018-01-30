using Discord;
using Discord.WebSocket;
using GuardianV4_Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static async Task<IRole> GetOrCreateMutedRole(this SocketGuild guild)
        {
            var mutedRole = guild.Roles.FirstOrDefault(role => role.Name.ToUpper() == "MUTED");

            if (mutedRole != null)
            {
                return mutedRole;
            }
            else
            {
                var restMutedRole = await guild.CreateRoleAsync("Muted", GuildPermissions.None, new Color(0xDD4646));
                foreach (var channel in guild.Channels)
                {
                    await channel.AddPermissionOverwriteAsync(restMutedRole,
                        new OverwritePermissions(
                            speak: PermValue.Deny,
                            sendMessages: PermValue.Deny,
                            addReactions: PermValue.Deny));
                }
                return restMutedRole;
            }
        }

        public static async Task<UserJoinQueue> GetUserJoinQueueAsync(this SocketGuild guild)
        {
            var autoModService = Program.Services.GetRequiredService<AutoModerationService>();

            if (autoModService.UserQueues.ContainsKey(guild.Id))
            {
                return autoModService.UserQueues[guild.Id];
            }
            else
            {
                return null;
            }
        }
    }
}
