using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
namespace Apollo_Core.Services
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
            _commandService.Log += LogCommand;

        }

        private ILoggerFactory ConfigureLogging(ILoggerFactory factory)
        {
            factory.AddConsole();
            return factory;
        }

        private Task LogDiscord(LogMessage arg)
        {
            Log(arg);

            return Task.CompletedTask;
        }
        private Task LogCommand(LogMessage arg)
        {
            if (arg.Exception is CommandException command)
            {
                var _ = command.Context.Channel.SendMessageAsync($"Error: {command.Message}");
            }

            Log(arg);
            return Task.CompletedTask;
        }

        private void Log(LogMessage arg)
        {
            _discordLogger.Log(
                            LogLevelFromSeverity(arg.Severity),
                            0,
                            arg.Message,
                            arg.Exception,
                            (_1, _2) => arg.ToString(prependTimestamp: true, timestampKind: DateTimeKind.Local));
        }

        private static LogLevel LogLevelFromSeverity(LogSeverity severity)
            => (LogLevel)(Math.Abs((int)severity - 5));

    }
}
