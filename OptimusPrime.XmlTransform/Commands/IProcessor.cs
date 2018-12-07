namespace OptimusPrime.Cli.Commands
{
    public interface IProcessor
    {
        void Start(string fileName, string arguments);
    }
}