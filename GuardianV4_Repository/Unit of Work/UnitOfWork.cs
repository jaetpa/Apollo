using GuardianV4_Data.Contexts;
using GuardianV4_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Repository.Unit_of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        ServerRepository _servers;
        QuoteRepository _quotes;
        public DiscordBotContext Context { get; }
        public ServerRepository Servers => _servers ?? (_servers = new ServerRepository(Context));
        public QuoteRepository Quotes => _quotes ?? (_quotes = new QuoteRepository(Context));

        public UnitOfWork(DiscordBotContext context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
