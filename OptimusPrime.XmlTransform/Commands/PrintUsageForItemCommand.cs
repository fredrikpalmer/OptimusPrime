namespace OptimusPrime.Cli.Commands
{
    internal class PrintUsageForItemCommand : Command
    {
        private readonly Command _command;

        public PrintUsageForItemCommand(Command command) : base($"{command.Name} -?", "Descriptive about a command")
        {
            _command = command;
        }

        public override void Execute()
        {
            PrintUsage();
        }

        public override void PrintUsage()
        {
            _command.PrintUsage();
        }
    }
}