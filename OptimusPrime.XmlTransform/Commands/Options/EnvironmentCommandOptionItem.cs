﻿using System;
using System.Linq;
using OptimusPrime.Cli.Constants;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands.Options
{
    public class EnvironmentCommandOptionItem : ICommandOptionItem
    {
        private readonly string[] _args;

        public string Option => ApplicationConstants.Options.Configuration.Argument;
        public string ShortOption => ApplicationConstants.Options.Configuration.ShortArgument;
        public string Description => ApplicationConstants.Options.Configuration.Description;

        public EnvironmentCommandOptionItem(string[] args)
        {
            _args = args;
        }

        public bool MatchesArgument(string argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));

            if (Option.Equals(argument, StringComparison.OrdinalIgnoreCase)) return true;
            if (ShortOption.Equals(argument, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }

        public string GetArgumentValue()
        {
            var argument = _args.FirstOrDefault(MatchesArgument);
            if (argument == null) return null;

            var commandOptionIndex = Array.IndexOf(_args, argument);

            var commandOptionValueIndex = commandOptionIndex + 1;
            if (commandOptionValueIndex >= _args.Length) return null;

            return _args[commandOptionValueIndex];
        }

        public void PrintUsage()
        {
            ConsoleLogger.Instance.Info($"{Option}, {ShortOption}\t{Description}");
        }
    }
}