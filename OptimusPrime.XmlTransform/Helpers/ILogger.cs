using System;

namespace OptimusPrime.Cli.Helpers
{
    public interface ILogger
    {
        void Info(string message, ConsoleColor color = ConsoleColor.White);
        void Error(string message);
    }

    public class ConsoleLogger : ILogger
    {
        private static ILogger _logger;

        public static ILogger Instance => _logger ?? (_logger = new ConsoleLogger());

        private readonly ConsoleColor _foregroundColor;

        private ConsoleLogger()
        {
            _foregroundColor = Console.ForegroundColor;
        }

        public void Info(string message, ConsoleColor color = ConsoleColor.White)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = _foregroundColor;
        }

        public void Error(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);

            Console.ForegroundColor = _foregroundColor;
        }
    }
}
