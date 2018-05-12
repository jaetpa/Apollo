using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo_Data.Entities
{
    public class KickedUserEntity : DatabaseEntity
    {
        ulong ID { get; set; }
        ulong ServerId { get; set; }
        string Username { get; set; }
        string ServerName { get; set; }
        DateTimeOffset LastKickTime { get; set; }
    }
}
