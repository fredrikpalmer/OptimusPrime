using System.Collections.Generic;
using System.Linq;
using OptimusPrime.Cli.Commands.Options;

namespace OptimusPrime.Cli.Helpers
{
    internal class MsBuildArgumentVisitor : IVisitor
    {
        public string[] Args { get; private set; }

        public void Visit(IList<ICommandOptionItem> commandOptionItems)
        {
            Args = GetArguments(commandOptionItems).ToArray();
        }

        private IEnumerable<string> GetArguments(IList<ICommandOptionItem> commandOptionItems)
        {
            foreach (var commandOptionItem in commandOptionItems)
            {
                yield return GetArgument(commandOptionItem);
            }
        }

        private string GetArgument(ICommandOptionItem commandOptionItem)
        {
            switch (commandOptionItem)
            {
                case PathCommandOptionItem path:
                    return $"/p:SourcePath={path.GetValue()}";
                case OutputCommandOptionItem output:
                    return $"/p:DestinationPath={output.GetValue()}";
                case EnvironmentCommandOptionItem environment:
                    return $"/p:Configuration={environment.GetValue()}";
                default:
                    return string.Empty;
            }
        }
    }
}