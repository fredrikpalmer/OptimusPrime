using System;
using System.Collections.Generic;
using System.Configuration;
using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli
{
    public class Program
    {
        static Program()
        {
            InitializerAction = ctx =>
            {
                ctx.UseLogger(ConsoleLogger.Instance);
                ctx.UseConfiguration(GetApplicationConfiguration());
                ctx.UseFileInfoService(new MsBuildFileInfoService());
                ctx.UseProcessor(new DefaultProcessor(ConsoleLogger.Instance));
            };
        }

        public static Action<IApplicationContextInitializer> InitializerAction { get; set; }

        public static void Main(string[] args)
        {
            var context = ApplicationContext.Configure(InitializerAction);

            var availableCommands = GetAvailableCommands(args);

            var commandResolver = new CommandResolver(availableCommands, context.Logger);
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
                new MsBuildTransformCommand(args, ApplicationContext.Current.Configuration, ApplicationContext.Current.Logger, ApplicationContext.Current.FileInfoService, ApplicationContext.Current.Processor)
            };
        }
    }
}
