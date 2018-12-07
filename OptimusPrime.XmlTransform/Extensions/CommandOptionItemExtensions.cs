using System;
using OptimusPrime.Cli.Commands.Options;

namespace OptimusPrime.Cli.Extensions
{
    public static class CommandOptionItemExtensions
    {
        public static T GetValue<T>(this ICommandOptionItem commandOptionItem)
        {
            if (TryGetConfiguredType(commandOptionItem, out T value)) return value;

            try
            {
                return (T)Convert.ChangeType(commandOptionItem.GetArgumentValue(), typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private static bool TryGetConfiguredType<T>(ICommandOptionItem commandOptionItem, out T value)
        {
            value = default(T);

            var commandOptionItemValue = commandOptionItem as ICommandOptionItemValue<T>;
            if (commandOptionItemValue == null) return false;

            value = commandOptionItemValue.GetValue();
            return true;

        }
    }
}