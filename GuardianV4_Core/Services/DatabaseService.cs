using GuardianV4_Data.Contexts;
using GuardianV4_Repository.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Core.Services
{
    public class DatabaseService
    {
        public UnitOfWork UnitOfWork => new UnitOfWork(new DiscordBotContext());

    }
}
