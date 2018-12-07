namespace OptimusPrime.Cli.Commands.Options
{
    public interface ICommandOptionItem
    {
        void PrintUsage();
        bool MatchesArgument(string argument);
        string GetArgumentValue();
    }
}