using DiscordBot_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Repository.Unit_of_Work
{
    interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
