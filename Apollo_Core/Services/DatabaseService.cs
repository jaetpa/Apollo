using DiscordBot_Data.Contexts;
using DiscordBot_Repository.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Core.Services
{
    public class DatabaseService
    {
        public UnitOfWork UnitOfWork => new UnitOfWork(new DiscordBotContext());

    }
}
