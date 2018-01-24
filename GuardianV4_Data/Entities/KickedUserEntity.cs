using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Data.Entities
{
    public class KickedUserEntity
    {
        ulong ID { get; set; }
        string Username { get; set; }
        DateTimeOffset LastKickTime { get; set; }
    }
}
