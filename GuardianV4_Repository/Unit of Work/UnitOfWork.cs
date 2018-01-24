using GuardianV4_Data.Contexts;
using GuardianV4_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Repository.Unit_of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        public DiscordBotContext Context { get; }
        public ServerRepository Servers { get; }
        public QuoteRepository Quotes { get; }

        public UnitOfWork(DiscordBotContext context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
