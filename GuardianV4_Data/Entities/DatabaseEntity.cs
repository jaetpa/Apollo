using System;
using Microsoft.EntityFrameworkCore;
namespace GuardianV4_Data.Entities
{
    public class DatabaseEntity
    {
        public ulong Id { get; set; }
        public DateTimeOffset DateAdded { get; set; }
    }
}