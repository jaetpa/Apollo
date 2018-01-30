using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GuardianV4_Core.Extensions;
using System;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("lockdown")]
        [Summary("Toggles lockdown mode where no new users can join the server.")]
        [Remarks("!lockdown")]
        public async Task ToggleLockdown()
        {
            using (var uow = _db.UnitOfWork)
            {
                var entity = uow.Servers.Find(Context.Guild.Id);

                if (entity?.Lockdown == null)
                {
                    return;
                }

                uow.Servers.Update(entity);
                entity.Lockdown = !entity.Lockdown;
                uow.SaveChanges();

                if (entity.Lockdown)
                {
                    await ReplyAsync("Lockdown mode enabled. New users will be blocked from joining.");

                    var embed = new EmbedBuilder()
                        .WithEmbedType(EmbedType.LockdownEnabled, Context.User)
                        .WithDescription($"User **{Context.User}** enabled Lockdown mode.")
                        .Build();
                    Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);

                }
                else
                {
                    await ReplyAsync("Lockdown mode disabled. New users will be allowed to join.");

                    var embed = new EmbedBuilder()
                        .WithEmbedType(EmbedType.LockdownDisabled, Context.User)
                        .WithDescription($"User **{Context.User}** disabled Lockdown mode.")
                        .Build();
                    Context.Guild.GetLogChannel()?.SendMessageAsync("", embed: embed);
                }

                await SetMainChannelTopic_Lockdown(entity, Context.Guild.GetMainChannel());
            }
        }

        public static async Task SetMainChannelTopic_Lockdown(GuardianV4_Data.Entities.ServerEntity entity, SocketTextChannel channel)
        {
            string lockdownString = ":warning: Lockdown Mode Enabled!";

            if (entity.Lockdown && channel != null)
            {
                await channel.ModifyAsync((TextChannelProperties x) => x.Topic = $"{lockdownString} {channel.Topic ?? String.Empty}");
            }
            else
            {
                if (channel != null)
                {
                    await channel.ModifyAsync((TextChannelProperties x) =>
                    {
                        if (channel.Topic.StartsWith($"{lockdownString} "))
                        {
                            x.Topic = $"{channel.Topic.Split($"{lockdownString} ")[1]}";
                        }
                    });
                }
            }
        }
    }
}

