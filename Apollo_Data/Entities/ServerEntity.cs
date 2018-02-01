using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Data.Entities
{
    public class ServerEntity : DatabaseEntity
    {
        public string GuildName { get; set; }
        public ulong? MainChannelId { get; set; }
        public ulong? WelcomeChannelId { get; set; }
        public ulong? StaffChannelId { get; set; }
        public ulong? BotChannelId { get; set; }
        public ulong? LogChannelId { get; set; }
        public ulong? DeleteLogChannelId { get; set; }
        public ulong? VoiceTextChannelId { get; set; }
        public ulong? StreamTextChannelId { get; set; }
        public bool Lockdown { get; set; } = false;
        public DateTimeOffset LockdownTime { get; set; }

        public virtual ICollection<QuoteEntity> Quotes { get; set; }
    }
}
