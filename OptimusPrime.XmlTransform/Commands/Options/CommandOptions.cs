using System;
using System.Collections.Generic;
using System.Linq;
using OptimusPrime.Cli.Extensions;

namespace OptimusPrime.Cli.Commands.Options
{
    public class CommandOptions 
    {
        private readonly IList<ICommandOptionItem> _commandOptionItems;

        public CommandOptions(IList<ICommandOptionItem> commandOptionItems)
        {
            _commandOptionItems = commandOptionItems ?? throw new ArgumentNullException(nameof(commandOptionItems));
        }

        public T GetArgumentValue<T>(string argument)
        {
            var commandOption = _commandOptionItems.FirstOrDefault(x => x.MatchesArgument(argument));
            if (commandOption == null) return default(T);

            return commandOption.GetValue<T>();
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