using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands
{
    internal class PrintUsageCommand : Command
    {
        private readonly Command[] _availableCommands;
        private readonly ILogger _logger;

        public PrintUsageCommand(Command[] availableCommands, ILogger logger) : base("-?       ", "Descriptive information about all available commands")
        {
            _availableCommands = availableCommands;
            _logger = logger;
        }

        public override void Execute()
        {
            PrintUsage();
        }

        public override void PrintUsage()
        {
            _logger.Info("Commands:");
            foreach (var command in _availableCommands)
            {
                _logger.Info($"{command.Name}\t{command.Description}");
            }

            _logger.Info($"{Name}\t{Description}");
        }
    }
}