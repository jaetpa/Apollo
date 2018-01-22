using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace GuardianV4_Core.Services
{
    public class LogService
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private ILoggerFactory _loggerFactory;
        private ILogger _discordLogger;
        private ILogger _commandsLogger;

        public LogService(DiscordSocketClient client, CommandService commandService, ILoggerFactory loggerFactory)
        {
            _client = client;
            _commandService = commandService;
            _loggerFactory = ConfigureLogging(loggerFactory);

            _discordLogger = loggerFactory.CreateLogger("discord");
            _commandsLogger = loggerFactory.CreateLogger("commands");


            _client.Log += LogDiscord;
            _client.Log += LogCommand;

        }

        private Task LogCommand(LogMessage arg)
        {
            _discordLogger.Log(
                LogLevelFromSeverity(arg.Severity),
                0,
                arg.Message,
                arg.Exception,
                (_1, _2) => arg.ToString(prependTimestamp:true, timestampKind:DateTimeKind.Local));

            return Task.CompletedTask;
        }

        private static LogLevel LogLevelFromSeverity(LogSeverity severity)
            => (LogLevel)(Math.Abs((int)severity - 5));

        private Task LogDiscord(LogMessage arg)
        {
            throw new NotImplementedException();
        }

        private ILoggerFactory ConfigureLogging(ILoggerFactory factory)
        {
            factory.AddConsole();
            return factory;
        }
    }
}
