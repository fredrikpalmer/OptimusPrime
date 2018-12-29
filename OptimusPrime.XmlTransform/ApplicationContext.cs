using System;
using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli
{
    public class ApplicationContext : IApplicationContextInitializer
    {
        public ILogger Logger { get; private set; }
        public IApplicationConfiguration Configuration { get; private set; }
        public IMsBuildFileInfoService FileInfoService { get; private set; }
        public IProcessor Processor { get; private set; }

        private ApplicationContext(): this(ConsoleLogger.Instance, null, new MsBuildFileInfoService(), new DefaultProcessor(ConsoleLogger.Instance)) { }

        private ApplicationContext(ILogger logger, IApplicationConfiguration configuration, IMsBuildFileInfoService fileInfoService, IProcessor processor)
        {
            Logger = logger;
            Configuration = configuration;
            FileInfoService = fileInfoService;
            Processor = processor;
        }

        public static ApplicationContext Current { get; private set; }

        public static ApplicationContext Configure(Action<IApplicationContextInitializer> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var context = new ApplicationContext();

            action(context);

            return Current ?? (Current = context);
        }

        void IApplicationContextInitializer.UseLogger(ILogger logger)
        {
            Logger = logger;
        }

        void IApplicationContextInitializer.UseConfiguration(IApplicationConfiguration configuration)
        {
            Configuration = configuration;
        }

        void IApplicationContextInitializer.UseFileInfoService(IMsBuildFileInfoService fileInfoService)
        {
            FileInfoService = fileInfoService;
        }

        void IApplicationContextInitializer.UseProcessor(IProcessor processor)
        {
            Processor = processor;
        }
    }
}
