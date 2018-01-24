using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianV4_Core
{
    public enum EmbedType
    {
        Join, Leave,
        LockdownEnabled, LockdownDisabled, LockdownKick,
        Kick, Ban, Warn,
        Mute, Unmute,
        Connect,
        Quote,
        VcJoin, VcSwitch, VcLeave,
        UsernameChange,NicknameChange
    }
    public class EmbedMessageBuilder
    {
        private EmbedBuilder _embedBuilder = null;

        public EmbedMessageBuilder()
        {
            _embedBuilder = new EmbedBuilder().WithColor(new Color(0x777777));

        }

        public EmbedMessageBuilder WithEmbedType(EmbedType embedType)
        {
            switch (embedType)
            {
                case EmbedType.Join:
                    _embedBuilder.Title = "User Joined";
                    _embedBuilder.Color = new Color(0x15C126);
                    break;
                case EmbedType.Leave:
                    _embedBuilder.Title = "User Left";
                    _embedBuilder.Color = new Color(0x0E7B19);
                    break;
                case EmbedType.LockdownEnabled:
                    _embedBuilder.Title = "Lockdown Enabled";
                    _embedBuilder.Color = new Color(0xE3C126);
                    break;
                case EmbedType.LockdownDisabled:
                    _embedBuilder.Title = "Lockdown Disabled";
                    _embedBuilder.Color = new Color(0xB5991E);
                    break;
                case EmbedType.LockdownKick:
                    _embedBuilder.Title = "User Kicked by Lockdown";
                    _embedBuilder.Color = new Color(0xE24926);
                    break;
                case EmbedType.Kick:
                    _embedBuilder.Title = "User Kicked";
                    _embedBuilder.Color = new Color(0xE27526);
                    break;
                case EmbedType.Ban:
                    _embedBuilder.Title = "User Banned";
                    _embedBuilder.Color = new Color(0xE21F1F);
                    break;
                case EmbedType.Warn:
                    _embedBuilder.Title = "User Warned";
                    _embedBuilder.Color = new Color(0xE2A72F);
                    break;
                case EmbedType.Mute:
                    _embedBuilder.Title = "User Muted";
                    _embedBuilder.Color = new Color(0xDD4646);
                    break;
                case EmbedType.Unmute:
                    _embedBuilder.Title = "User Unmuted";
                    _embedBuilder.Color = new Color(0x8E2D2D);
                    break;
                case EmbedType.Connect:
                    _embedBuilder.Title = "Bot Connected";
                    _embedBuilder.Color = new Color(0xDBDBDB);
                    break;
                case EmbedType.Quote:
                    _embedBuilder.Title = "Quote";
                    _embedBuilder.Color = new Color(0xF2AEAE);
                    break;
                case EmbedType.VcJoin:
                    _embedBuilder.Title = "User Joined Voice Channel";
                    _embedBuilder.Color = new Color(0xDBDBDB);
                    break;
                case EmbedType.VcSwitch:
                    _embedBuilder.Title = "User Switched Voice Channel";
                    _embedBuilder.Color = new Color(0xADADAD);
                    break;
                case EmbedType.VcLeave:
                    _embedBuilder.Title = "User Left Voice Channel";
                    _embedBuilder.Color = new Color(0x707070);
                    break;
                case EmbedType.UsernameChange:
                    _embedBuilder.Title = "User Changed Username";
                    _embedBuilder.Color = new Color(0x38A4B5);
                    
                    break;
                case EmbedType.NicknameChange:
                    _embedBuilder.Title = "User Changed Nickname";
                    _embedBuilder.Color = new Color(0x47D0E5);
                    break;
            }
            return this;
        }

        public EmbedMessageBuilder WithTimestamp()
        {
            if (_embedBuilder.Footer == null)
            {
                _embedBuilder.WithFooter(new EmbedFooterBuilder());
            }

            _embedBuilder.Footer.Text += $" | {DateTimeOffset.Now:ddd dd MMM, yyyy HH:mm:ss}";

            return this;
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }
    }
}
