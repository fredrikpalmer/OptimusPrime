using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands.Options
{
    public interface ICommandOptions
    {
        void Accept(IVisitor visitor);

        void PrintUsage();
    }
}