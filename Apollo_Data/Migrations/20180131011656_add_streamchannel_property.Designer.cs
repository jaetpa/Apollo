﻿// <auto-generated />
using DiscordBot_Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DiscordBot_Data.Migrations
{
    [DbContext(typeof(DiscordBotContext))]
    [Migration("20180131011656_add_streamchannel_property")]
    partial class add_streamchannel_property
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DiscordBot_Data.Entities.QuoteEntity", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateAdded");

                    b.Property<DateTimeOffset>("DateUpdated");

                    b.Property<string>("Name");

                    b.Property<string>("Quote");

                    b.Property<ulong?>("ServerEntityId");

                    b.HasKey("Id");

                    b.HasIndex("ServerEntityId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("DiscordBot_Data.Entities.ServerEntity", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong?>("BotChannelId");

                    b.Property<DateTimeOffset>("DateAdded");

                    b.Property<DateTimeOffset>("DateUpdated");

                    b.Property<ulong?>("DeleteLogChannelId");

                    b.Property<string>("GuildName");

                    b.Property<bool>("Lockdown");

                    b.Property<ulong?>("LogChannelId");

                    b.Property<ulong?>("MainChannelId");

                    b.Property<ulong?>("StaffChannelId");

                    b.Property<ulong?>("StreamTextChannelId");

                    b.Property<ulong?>("VoiceTextChannelId");

                    b.Property<ulong?>("WelcomeChannelId");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("DiscordBot_Data.Entities.QuoteEntity", b =>
                {
                    b.HasOne("DiscordBot_Data.Entities.ServerEntity")
                        .WithMany("Quotes")
                        .HasForeignKey("ServerEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
