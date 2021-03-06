﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apollo_Core
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
        UsernameChange, NicknameChange
    }
    public static class EmbedMessageExtensions
    {
        public static EmbedBuilder WithEmbedType(this EmbedBuilder embedBuilder, EmbedType embedType, IUser user)
        {
            switch (embedType)
            {
                case EmbedType.Join:
                    embedBuilder.Title = "User Joined";
                    embedBuilder.Color = new Color(0x15C126);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} joined the server")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();
                    break;

                case EmbedType.Leave:
                    embedBuilder.Title = "User Left";
                    embedBuilder.Color = new Color(0x0E7B19);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} left the server")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();
                    break;

                case EmbedType.LockdownEnabled:
                    embedBuilder.Title = "Lockdown Enabled";
                    embedBuilder.Color = new Color(0xE3C126);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} enabled Lockdown mode")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();
                    break;
                case EmbedType.LockdownDisabled:
                    embedBuilder.Title = "Lockdown Disabled";
                    embedBuilder.Color = new Color(0xB5991E);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} disabled Lockdown mode")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();

                    break;
                case EmbedType.LockdownKick:
                    embedBuilder.Title = "User Kicked by Lockdown";
                    embedBuilder.Color = new Color(0xE24926);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                .WithText($"User {user.Id} blocked by Lockdown mode")
                                .WithIconUrl(user.GetAvatarUrl()))
                            .WithTimestamp();
                    break;
                case EmbedType.Kick:
                    embedBuilder.Title = "User Kicked";
                    embedBuilder.Color = new Color(0xE27526);
                    break;
                case EmbedType.Ban:
                    embedBuilder.Title = "User Banned";
                    embedBuilder.Color = new Color(0xE21F1F);
                    break;
                case EmbedType.Warn:
                    embedBuilder.Title = "User Warned";
                    embedBuilder.Color = new Color(0xE2A72F);
                    break;
                case EmbedType.Mute:
                    embedBuilder.Title = "User Muted";
                    embedBuilder.Color = new Color(0xDD4646);
                    break;
                case EmbedType.Unmute:
                    embedBuilder.Title = "User Unmuted";
                    embedBuilder.Color = new Color(0x8E2D2D);
                    break;
                case EmbedType.Connect:
                    embedBuilder.Title = "Bot Connected";
                    embedBuilder.Color = new Color(0xDBDBDB);
                    embedBuilder.WithDescription($"Apollo conected. I'm online!");
                    embedBuilder.WithTimestamp();
                    embedBuilder.WithFooter(
                        (embedBuilder.Footer ?? new EmbedFooterBuilder()).WithIconUrl(user.GetAvatarUrl()));
                    break;
                case EmbedType.Quote:
                    embedBuilder.Title = "Quote";
                    embedBuilder.Color = new Color(0xF2AEAE);
                    break;
                case EmbedType.VcJoin:
                    embedBuilder.Title = "User Joined Voice Channel";
                    embedBuilder.Color = new Color(0xDBDBDB);
                    break;
                case EmbedType.VcSwitch:
                    embedBuilder.Title = "User Switched Voice Channel";
                    embedBuilder.Color = new Color(0xADADAD);
                    break;
                case EmbedType.VcLeave:
                    embedBuilder.Title = "User Left Voice Channel";
                    embedBuilder.Color = new Color(0x707070);
                    break;
                case EmbedType.UsernameChange:
                    embedBuilder.Title = "User Changed Username";
                    embedBuilder.Color = new Color(0x38A4B5);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} changed username")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();
                    break;
                case EmbedType.NicknameChange:
                    embedBuilder.Title = "User Changed Nickname";
                    embedBuilder.Color = new Color(0x47D0E5);
                    embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                                    .WithText($"User {user.Id} changed nickname")
                                    .WithIconUrl(user.GetAvatarUrl()))
                                .WithTimestamp();
                    break;
            }
            return embedBuilder;
        }

        public static EmbedBuilder WithTimestamp(this EmbedBuilder embedBuilder)
        {
            if (embedBuilder.Footer == null)
            {
                embedBuilder.WithFooter(new EmbedFooterBuilder());
            }

            embedBuilder.Footer.Text += $" | {DateTimeOffset.Now:ddd dd MMM, yyyy HH:mm:ss}";

            return embedBuilder;
        }
    }
}
