using DiscordBot_Data.Contexts;
using DiscordBot_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DiscordBot_Repository.Repositories
{
    public class QuoteRepository : IRepository<QuoteEntity>
    {
        DiscordBotContext _context;

        public QuoteRepository(DiscordBotContext context)
        {
            _context = context;
        }

        public void Add(QuoteEntity entity)
        {
            _context.Quotes.Add(entity);
        }

        public QuoteEntity Find(QuoteEntity entity)
        {
            return _context.Quotes.Find(entity.Id);
        }

        public QuoteEntity Find(ulong id)
        {
            return _context.Quotes.Find(id);
        }

        public IEnumerable<QuoteEntity> Find(Func<QuoteEntity, bool> predicate)
        {
            return _context.Quotes.Where(predicate);
        }

        public void Remove(QuoteEntity entity)
        {
            _context.Quotes.Remove(entity);
        }

        public void Update(QuoteEntity entity)
        {
            _context.Quotes.Update(entity);
        }
    }
}
