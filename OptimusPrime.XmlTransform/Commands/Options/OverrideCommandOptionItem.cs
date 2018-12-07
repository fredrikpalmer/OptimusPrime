using System;
using System.Linq;
using OptimusPrime.Cli.Constants;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands.Options
{
    public class OverrideCommandOptionItem : ICommandOptionItem, ICommandOptionItemValue<bool>
    {
        private readonly string[] _args;

        public string Option => ApplicationConstants.Options.Override.Argument;
        public string Description => ApplicationConstants.Options.Override.Description;

        public OverrideCommandOptionItem(string[] args)
        {
            _args = args;
        }

        public bool MatchesArgument(string argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));

            if (!Option.Equals(argument, StringComparison.OrdinalIgnoreCase)) return false;

            return _args.Any(argument.Equals);
        }

        public string GetArgumentValue() => GetValue().ToString();

        public bool GetValue() => MatchesArgument(Option);

        public void PrintUsage() => ConsoleLogger.Instance.Info($"{Option}\t{Description}");
    }
}