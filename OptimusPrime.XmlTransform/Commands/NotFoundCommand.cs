using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands
{
    public class NotFoundCommand : Command
    {
        private readonly ILogger _logger;

        public NotFoundCommand(string argument, ILogger logger) : base("Error", $"No command found matching \"{argument}\"")
        {
            _logger = logger;
        }

        public override void Execute()
        {
            PrintUsage();
        }

        public override void PrintUsage()
        {
            _logger.Error(Description);
        }
    }
}
