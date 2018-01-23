using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord.Addons.MicrosoftLogging;
using Microsoft.Extensions.Logging;
using Discord.Commands;
using GuardianV4_Core.Services;
using Discord.WebSocket;
using System.IO;
using Discord;
using GuardianV4_Core.Modules;

namespace GuardianV4_Core
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
#if DEBUG
                token = _config["DebugToken"];
#elif RELEASE
                token = _config["ReleaseToken"];
#endif
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
            Services.GetRequiredService<ModeratorModule>();

            await Task.Delay(-1);
        }


        private IServiceProvider AddServices()
        {
            return new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandlingService>()
            .AddSingleton<ModeratorModule>()
            .AddLogging()
            .AddSingleton<LogService>()
            .AddSingleton(_config)
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
