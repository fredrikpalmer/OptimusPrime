using OptimusPrime.Cli.Commands;

namespace OptimusPrime.Cli
{
    public interface ICommandResolver
    {
        Command Resolve(string[] args);
    }
}