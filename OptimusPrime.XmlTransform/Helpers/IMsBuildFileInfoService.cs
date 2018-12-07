using System.Collections.Generic;
using OptimusPrime.Cli.Commands.Options;
using OptimusPrime.Cli.Models;

namespace OptimusPrime.Cli.Helpers
{
    public interface IMsBuildFileInfoService
    {
        IEnumerable<FileInformation> GetAllFileInfo(CommandOptions commandOptions);
    }
}