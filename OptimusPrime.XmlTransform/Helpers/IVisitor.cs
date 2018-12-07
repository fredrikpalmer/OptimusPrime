using System.Collections.Generic;
using OptimusPrime.Cli.Commands.Options;

namespace OptimusPrime.Cli.Helpers
{
    public interface IVisitor
    {
        void Visit(IList<ICommandOptionItem> commandOptionItems);
    }
}