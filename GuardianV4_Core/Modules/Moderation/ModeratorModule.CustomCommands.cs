using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using GuardianV4_Core.Services;
using Newtonsoft.Json;

namespace GuardianV4_Core.Modules.Moderation
{
    public partial class ModeratorModule
    {
        [Command("addcommand")]
        public async Task AddCommand(string commandName)
        {
            using (var uow = _db.UnitOfWork)
            {
                if (uow.CustomCommands.Find())
                {

                }
            }
        }
    }
}
