using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Data.Entities
{
    public class ServerEntity
    {
        public ulong Id { get; set; }
        public ulong MainChannelId { get; set; }
        public ulong LogChannelId { get; set; }
        public ulong BotChannelId { get; set; }
        public ulong StaffChannelId { get; set; }

        public virtual ICollection<QuoteEntity> Quotes { get; set; }
    }
}
