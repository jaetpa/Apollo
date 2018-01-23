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
                    _embedBuilder.Color = new Color(0x15C126);
                    break;
                case EmbedType.Leave:
                    _embedBuilder.Color = new Color(0x0E7B19);
                    break;
                case EmbedType.LockdownEnabled:
                    _embedBuilder.Color = new Color(0xE3C126);
                    break;
                case EmbedType.LockdownDisabled:
                    _embedBuilder.Color = new Color(0xB5991E);
                    break;
                case EmbedType.LockdownKick:
                    _embedBuilder.Color = new Color(0xE24926);
                    break;
                case EmbedType.Kick:
                    _embedBuilder.Color = new Color(0xE27526);
                    break;
                case EmbedType.Ban:
                    _embedBuilder.Color = new Color(0xE21F1F);
                    break;
                case EmbedType.Warn:
                    _embedBuilder.Color = new Color(0xE2A72F);
                    break;
                case EmbedType.Mute:
                    _embedBuilder.Color = new Color(0xDD4646);
                    break;
                case EmbedType.Unmute:
                    _embedBuilder.Color = new Color(0x8E2D2D);
                    break;
                case EmbedType.Connect:
                    _embedBuilder.Color = new Color(0xDBDBDB);
                    break;
                case EmbedType.Quote:
                    _embedBuilder.Color = new Color(0xF2AEAE);
                    break;
                case EmbedType.VcJoin:
                    _embedBuilder.Color = new Color(0xDBDBDB);
                    break;
                case EmbedType.VcSwitch:
                    _embedBuilder.Color = new Color(0xADADAD);
                    break;
                case EmbedType.VcLeave:
                    _embedBuilder.Color = new Color(0x707070);
                    break;
                case EmbedType.UsernameChange:
                    _embedBuilder.Color = new Color(0x707070);
                    break;
                case EmbedType.NicknameChange:
                    _embedBuilder.Color = new Color(0x707070);
                    break;
                default:
                    break;
            }
            return this;
        }


        public Embed Build()
        {
            return _embedBuilder.Build();
        }
    }
}
