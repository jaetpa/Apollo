using Apollo_Data.Contexts;
using Apollo_Repository.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo_Core.Services
{
    public class DatabaseService
    {
        public UnitOfWork UnitOfWork => new UnitOfWork(new DiscordBotContext());

    }
}
