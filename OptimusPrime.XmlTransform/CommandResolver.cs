using System;
using System.Linq;
using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli
{
    public class CommandResolver : ICommandResolver
    {
        private readonly Command[] _availableCommands;
        private readonly ILogger _logger;

        public CommandResolver(Command[] availableCommands, ILogger logger)
        {
            _availableCommands = availableCommands;
            _logger = logger;
        }

        public Command Resolve(string[] args)
        {
            if (args.Length == 0) return new PrintUsageCommand(_availableCommands, _logger);
            if (args[0] == "-?") return new PrintUsageCommand(_availableCommands, _logger);

            return GetCommand(args);
        }

        private Command GetCommand(string[] args)
        {
            var command = _availableCommands.FirstOrDefault(x => IsMatch(x, args[0]));
            if (command == null) return new NotFoundCommand(args[0], _logger);
            if (args.Contains("-?")) return new PrintUsageForItemCommand(command);

            return command;
        }

        private static bool IsMatch(Command command, string argument)
        {
            if (command.Name.Equals(argument, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }
}