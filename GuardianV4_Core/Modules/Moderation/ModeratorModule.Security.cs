using Discord;
using Discord.Commands;
using GuardianV4_Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule : ModuleBase<SocketCommandContext>
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

                string lockdownString = ":warning: Lockdown Mode Enabled!";
                if (entity.Lockdown)
                {
                    var mainChannel = Context.Guild.GetMainChannel();
                    if (mainChannel != null)
                    {
                        await mainChannel.ModifyAsync((TextChannelProperties x) => x.Topic = $"{lockdownString} {mainChannel.Topic ?? String.Empty}");
                    }
                }
                else
                {
                    var mainChannel = Context.Guild.GetMainChannel();
                    if (mainChannel != null)
                    {
                        await mainChannel.ModifyAsync((TextChannelProperties x) =>
                        {
                            if (mainChannel.Topic.StartsWith($"{lockdownString} "))
                            {
                                x.Topic = $"{mainChannel.Topic.Split($"{lockdownString} ")[1]}";
                            }
                        });
                    }
                }
            }
        }


    }
}

