using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli
{
    public interface IApplicationContextInitializer
    {
        void UseLogger(ILogger logger);
        void UseConfiguration(IApplicationConfiguration configuration);
        void UseFileInfoService(IMsBuildFileInfoService fileInfoService);
        void UseProcessor(IProcessor processor);
    }
}