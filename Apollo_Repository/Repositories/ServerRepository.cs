using Apollo_Data.Contexts;
using Apollo_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Apollo_Repository.Repositories
{
    public class ServerRepository : IRepository<ServerEntity>
    {
        DiscordBotContext _context;

        public ServerRepository(DiscordBotContext context)
        {
            _context = context;
        }

        public void Add(ServerEntity entity)
        {
            entity.DateAdded = DateTimeOffset.Now;
            entity.DateUpdated = DateTimeOffset.Now;
            _context.Servers.Add(entity);
        }

        public ServerEntity Find(ServerEntity entity)
        {
            return _context.Servers.Find(entity.Id);
        }

        public ServerEntity Find(ulong id)
        {
            return _context.Servers.Find(id);
        }

        public IEnumerable<ServerEntity> Find(Func<ServerEntity, bool> predicate)
        {
            return _context.Servers.Where(predicate);
        }

        public void Remove(ServerEntity entity)
        {
            _context.Servers.Remove(entity);
        }

        public void Update(ServerEntity entity)
        {
            entity.DateUpdated = DateTimeOffset.Now;
            _context.Servers.Update(entity);
        }
    }
}
