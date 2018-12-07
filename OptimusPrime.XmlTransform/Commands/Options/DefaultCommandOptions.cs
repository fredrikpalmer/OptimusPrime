using System;
using System.Collections.Generic;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands.Options
{
    public class DefaultCommandOptions : ICommandOptions
    {
        private readonly IList<ICommandOptionItem> _commandOptionItems;

        public DefaultCommandOptions(IList<ICommandOptionItem> commandOptionItems)
        {
            _commandOptionItems = commandOptionItems ?? throw new ArgumentNullException(nameof(commandOptionItems));
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            visitor.Visit(_commandOptionItems);
        }

        public void PrintUsage()
        {
            foreach (var commandOptionItem in _commandOptionItems)
            {
                commandOptionItem.PrintUsage();
            }
        }
    }
}