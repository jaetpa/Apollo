using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DiscordBot_Core.Services
{
    public class RoleTimerService
    {
        Timer _timer;
        DiscordSocketClient _client;
        SocketGuild _guild;

        public RoleTimerService(DiscordSocketClient client)
        {
            _client = client;
            _guild = client.GetGuild(351118984327856169);
            //var timeUntilDue = TimeSpan.FromHours(DateTimeOffset.Now.AddHours(1).Hour) - TimeSpan.FromMinutes(DateTimeOffset.Now.Minute);
            _timer = new Timer(CheckUserRoles, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        private async void CheckUserRoles(object state)
        {
            if (_guild == null)
            {
                return;
            }
            var futureFriendosRole = _guild?.Roles.FirstOrDefault(x => x.Name.ToUpper() == "FUTURE FRIENDOS");
            var friendosRole = _guild?.Roles.FirstOrDefault(x => x.Name.ToUpper() == "FRIENDOS");

            var futureFriendos = _guild?.Users.Where(x => x.Roles.Any(y => y.Name.ToUpper() == "FUTURE FRIENDOS"));

            foreach (var user in futureFriendos)
            {
                if (DateTimeOffset.Now - user.JoinedAt > TimeSpan.FromDays(7))
                {
                    await user.RemoveRoleAsync(futureFriendosRole);
                    await user.AddRoleAsync(friendosRole);
                }
            }
        }
    }
}
