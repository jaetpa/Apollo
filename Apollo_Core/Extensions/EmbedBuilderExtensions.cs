using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot_Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Core
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
        UsernameChange, NicknameChange,
        General,
        MessageEdited, MessageDeleted,
        Stream
    }
    public static class EmbedMessageExtensions
    {
        public static EmbedBuilder WithEmbedType(this EmbedBuilder embedBuilder, EmbedType embedType, IUser userToDisplay)
        {
            switch (embedType)
            {
                case EmbedType.Join:
                    embedBuilder.Title = "User Joined";
                    embedBuilder.Color = new Color(0x15C126);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Leave:
                    embedBuilder.Title = "User Left";
                    embedBuilder.Color = new Color(0x0E7B19);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.LockdownEnabled:
                    embedBuilder.Title = "Lockdown Enabled";
                    embedBuilder.Color = new Color(0xE3C126);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.LockdownDisabled:
                    embedBuilder.Title = "Lockdown Disabled";
                    embedBuilder.Color = new Color(0xB5991E);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.LockdownKick:
                    embedBuilder.Title = "User Kicked by Lockdown";
                    embedBuilder.Color = new Color(0xE24926);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Kick:
                    embedBuilder.Title = "User Kicked";
                    embedBuilder.Color = new Color(0xE27526);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Ban:
                    embedBuilder.Title = "User Banned";
                    embedBuilder.Color = new Color(0xE21F1F);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Warn:
                    embedBuilder.Title = "User Warned";
                    embedBuilder.Color = new Color(0xE2A72F);
                    break;

                case EmbedType.Mute:
                    embedBuilder.Title = "User Muted";
                    embedBuilder.Color = new Color(0xDD4646);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Unmute:
                    embedBuilder.Title = "User Unmuted";
                    embedBuilder.Color = new Color(0x8E2D2D);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Connect:
                    embedBuilder.Title = "Bot Connected";
                    embedBuilder.Color = new Color(0xDBDBDB);
                    embedBuilder.WithDescription($"Apollo connected. I'm online!");
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Quote:
                    embedBuilder.Title = "Quote";
                    embedBuilder.Color = new Color(0xF2AEAE);
                    break;

                case EmbedType.VcJoin:
                    embedBuilder.Title = "User Joined Voice Channel";
                    embedBuilder.Color = new Color(0xDBDBDB);
                    break;

                case EmbedType.MessageEdited:
                    embedBuilder.Title = "Message Edited";
                    embedBuilder.Color = new Color(0x96DDF7);
                    break;

                case EmbedType.MessageDeleted:
                    embedBuilder.Title = "Message Deleted";
                    embedBuilder.Color = new Color(0x59CDF7);
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
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.NicknameChange:
                    embedBuilder.Title = "User Changed Nickname";
                    embedBuilder.Color = new Color(0x47D0E5);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.Stream:
                    embedBuilder.Title = "Stream started";
                    embedBuilder.Color = new Color(0x6441A4);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

                case EmbedType.General:
                    embedBuilder.Title = "Log Event";
                    embedBuilder.Color = new Color(0xDBDBDB);
                    WithFooter(embedBuilder, userToDisplay);
                    break;

            }
            return embedBuilder;
        }

        private static EmbedBuilder WithFooter(this EmbedBuilder embedBuilder, IUser userToDisplay)
        {
            embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                            .WithText($"User {userToDisplay.Id} | ")
                            .WithIconUrl(userToDisplay.GetAvatarUrl()))
                        .WithTimestamp();
            return embedBuilder;
        }

        public static EmbedBuilder WithMessageLogFooter(this EmbedBuilder embedBuilder, IUser userToDisplay, AutoModerationService.MessageObject message)
        {
            embedBuilder.WithFooter((embedBuilder.Footer ?? new EmbedFooterBuilder())
                            .WithText($"User {userToDisplay.Id} | Message ID {message.Id}")
                            .WithIconUrl(userToDisplay.GetAvatarUrl()))
                        .WithTimestamp();
            return embedBuilder;
        }

        public static EmbedBuilder WithTimestamp(this EmbedBuilder embedBuilder)
        {
            if (embedBuilder.Footer == null)
            {
                embedBuilder.WithFooter(new EmbedFooterBuilder());
            }

            embedBuilder.Footer.Text += $"{DateTimeOffset.Now:ddd dd MMM, yyyy HH:mm:ss}";

            return embedBuilder;
        }
    }
}
