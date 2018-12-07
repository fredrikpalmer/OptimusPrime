namespace OptimusPrime.Cli.Config
{
    public interface IApplicationConfiguration
    {
        string GetValue(string key);
    }
}
