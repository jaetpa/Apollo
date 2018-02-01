using Apollo_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Apollo_Data.Contexts
{
    public class DiscordBotContext:DbContext
    {
        public DbSet<ServerEntity> Servers { get; set; }
        public DbSet<QuoteEntity> Quotes { get; set; }
        public DbSet<CustomCommandEntity> CustomCommands { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            options.UseSqlite($"Data Source={path}\\Apollo\\Apollo.db;");
        }
    }
}
