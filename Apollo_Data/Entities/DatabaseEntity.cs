using System;
using Microsoft.EntityFrameworkCore;
namespace DiscordBot_Data.Entities
{
    public class DatabaseEntity
    {
        public ulong Id { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset DateUpdated { get; set; }

    }
}