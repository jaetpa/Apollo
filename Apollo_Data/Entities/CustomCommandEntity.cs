using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Data.Entities
{
    public class CustomCommandEntity : DatabaseEntity
    {
        public string Command { get; set; }
        public string Reply { get; set; }
        public ulong GuildId { get; set; }
    }
}
