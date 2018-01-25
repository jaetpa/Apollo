using GuardianV4_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuardianV4_Data.Contexts
{
    public class DiscordBotContext:DbContext
    {
        public DbSet<ServerEntity> Servers { get; set; }
        public DbSet<QuoteEntity> Quotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            options.UseSqlite($"Data Source={path}\\GuardianV4\\GuardianV4.db;");
        }
    }
}
