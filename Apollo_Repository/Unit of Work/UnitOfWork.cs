using Apollo_Data.Contexts;
using Apollo_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo_Repository.Unit_of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        ServerRepository _servers;
        QuoteRepository _quotes;
        CustomCommandRepository _customCommands;
        public DiscordBotContext Context { get; }
        public ServerRepository Servers => _servers ?? (_servers = new ServerRepository(Context));
        public QuoteRepository Quotes => _quotes ?? (_quotes = new QuoteRepository(Context));
        public CustomCommandRepository CustomCommands => _customCommands ?? (_customCommands = new CustomCommandRepository(Context));


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
