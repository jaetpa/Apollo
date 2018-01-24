using GuardianV4_Data.Contexts;
using GuardianV4_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GuardianV4_Repository.Repositories
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
            _context.Servers.Update(entity);
        }
    }
}
