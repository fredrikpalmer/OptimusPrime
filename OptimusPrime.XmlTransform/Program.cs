using System.Collections.Generic;
using System.Configuration;
using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli
{
    public class Program
    {
        public static ILogger Logger { get; set; } = ConsoleLogger.Instance;
        public static IApplicationConfiguration Configuration { get; set; } = GetApplicationConfiguration();
        public static IMsBuildFileInfoService FileInfoService { get; set; } = new MsBuildFileInfoService();
        public static IProcessor Processor { get; set; } = new DefaultProcessor(ConsoleLogger.Instance);

        public static void Main(string[] args)
        {
            var availableCommands = GetAvailableCommands(args);

            var commandResolver = new CommandResolver(availableCommands, Logger);
            var command = commandResolver.Resolve(args);

            command.Execute();
        }

        private static IApplicationConfiguration GetApplicationConfiguration()
        {
            var dictionary = GetConfigValues();
            return new DefaultApplicationConfiguration(dictionary);
        }

        private static Dictionary<string, string> GetConfigValues()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                dictionary.Add(key, ConfigurationManager.AppSettings[key]);
            }
            return dictionary;
        }

        private static Command[] GetAvailableCommands(string[] args)
        {
            return new Command[]
            {
                new MsBuildTransformCommand(args, Configuration, Logger, FileInfoService, Processor)
            };
        }
    }
}
