﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Discord.Commands;
using Apollo_Core.Services;
using Discord.WebSocket;
using System.IO;
using Discord;
using Apollo_Core.Modules.Moderation;

namespace Apollo_Core
{
    class Program
    {
        public IServiceProvider Services { get; private set; }
        public ILogger Logger { get; private set; } = new LoggerFactory().CreateLogger("");
        public string Token
        {
            get
            {
                var token = String.Empty;
                token = _config["Token"];
                return token;
            }
        }
        private IConfiguration _config;
        private DiscordSocketClient _client;


        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _config = BuildConfig();

            DiscordSocketConfig socketConfig = new DiscordSocketConfig { AlwaysDownloadUsers = true };
            _client = new DiscordSocketClient(socketConfig);
            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            Services = AddServices();
            Services.GetRequiredService<LogService>();
            await Services.GetRequiredService<CommandHandlingService>().InitializeAsync(Services);
            Services.GetRequiredService<DatabaseService>();
            Services.GetRequiredService<GuildSetupService>();
            Services.GetRequiredService<LogChannelService>();
            Services.GetRequiredService<AutoModerationService>();

            await Task.Delay(-1);
        }


        private IServiceProvider AddServices()
        {
            return new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_config)
            .AddLogging()
            .AddSingleton<LogService>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandlingService>()
            .AddSingleton<DatabaseService>()
            .AddSingleton<GuildSetupService>()
            .AddSingleton<LogChannelService>()
            .AddSingleton<AutoModerationService>()
            .AddSingleton<ModeratorModule>()
            .BuildServiceProvider();
        }

        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("tokens.json")
                .Build();
        }
    }
}
