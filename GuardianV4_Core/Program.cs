using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord.Addons.MicrosoftLogging;
using Microsoft.Extensions.Logging;

namespace GuardianV4_Core
{
    class Program
    {
        public IServiceCollection Services { get; private set; } = new ServiceCollection();
        public ILogger Logger { get; private set; } = new LoggerFactory().CreateLogger("");



        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            AddServices();
            await Task.Delay(-1);
        }


        private void AddServices()
        {
            Services.AddSingleton<Discord.Addons.MicrosoftLogging.LogAdapter>();
            Services.BuildServiceProvider();
        }
    }
}
