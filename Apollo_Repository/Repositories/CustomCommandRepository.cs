using Apollo_Data.Contexts;
using Apollo_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Apollo_Repository.Repositories
{
    public class CustomCommandRepository : IRepository<CustomCommandEntity>
    {
        DiscordBotContext _context;

        public CustomCommandRepository(DiscordBotContext context)
        {
            _context = context;
        }

        public void Add(CustomCommandEntity entity)
        {
            entity.DateAdded = DateTimeOffset.Now;
            entity.DateUpdated = DateTimeOffset.Now;
            _context.CustomCommands.Add(entity);
        }

        public CustomCommandEntity Find(CustomCommandEntity entity)
        {
            return _context.CustomCommands.Find(entity.Id);
        }

        public CustomCommandEntity Find(ulong id)
        {
            return _context.CustomCommands.Find(id);
        }

        public IEnumerable<CustomCommandEntity> Find(Func<CustomCommandEntity, bool> predicate)
        {
            return _context.CustomCommands.Where(predicate);
        }

        public void Remove(CustomCommandEntity entity)
        {
            _context.CustomCommands.Remove(entity);
        }

        public void Update(CustomCommandEntity entity)
        {
            entity.DateUpdated = DateTimeOffset.Now;
            _context.CustomCommands.Update(entity);
        }
    }
}
