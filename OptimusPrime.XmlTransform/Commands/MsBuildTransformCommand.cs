using System;
using System.Linq;
using OptimusPrime.Cli.Commands.Options;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;
using OptimusPrime.Cli.Models;
using static OptimusPrime.Cli.Constants.ApplicationConstants.Commands;

namespace OptimusPrime.Cli.Commands
{
    internal class MsBuildTransformCommand : Command
    {
        private readonly CommandOptions _commandOptions;
        private readonly IApplicationConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMsBuildFileInfoService _msBuildFileInfoService;
        private readonly IProcessor _processor;

        public MsBuildTransformCommand(string[] args, IApplicationConfiguration configuration, ILogger logger, IMsBuildFileInfoService msBuildFileInfoService, IProcessor processor) : base(Transform.Argument, "Transform xml files for use in different environments")
        {
            _commandOptions = new CommandOptions(GetAvailableCommandOptionItems(args));

            _configuration = configuration;
            _logger = logger;
            _msBuildFileInfoService = msBuildFileInfoService;
            _processor = processor;
        }

        public override void Execute()
        {
            var files = _msBuildFileInfoService.GetAllFileInfo(_commandOptions).ToList();
            _logger.Info($"Files to process: {files.Count}{Environment.NewLine}", ConsoleColor.Yellow);

            foreach (var fileInfo in files)
            {
                _logger.Info($"Begin processing {GetFilePathForLogStatement(fileInfo)}{Environment.NewLine}", ConsoleColor.Yellow);

                var options = new MsBuildCommandBuilder()
                    .WithMsBuildFileName(_configuration.GetValue("MSBuildFileName"))
                    .WithTargetName(_configuration.GetValue("MSBuildTargetName"))
                    .WithFileInfo(fileInfo)
                    .Build();

                _logger.Info($"Running msbuild with arguments: {options}{Environment.NewLine}", ConsoleColor.Yellow);
                
                _processor.Start(_configuration.GetValue("MSBuildPath"), options);

                _logger.Info($"Finished processing {GetFilePathForLogStatement(fileInfo)}{Environment.NewLine}", ConsoleColor.Yellow);
            }
        }

        private string GetFilePathForLogStatement(FileInformation fileInfo)
        {
            return fileInfo.IsOverride
                ? fileInfo.TransformFile
                : fileInfo.SourceFile;
        }

        public override void PrintUsage()
        {
            _logger.Info($"{Name}:");

            _commandOptions.PrintUsage();
        }

        private static ICommandOptionItem[] GetAvailableCommandOptionItems(string[] args)
        {
            return new ICommandOptionItem[]
            {
                new PathCommandOptionItem(args),
                new EnvironmentCommandOptionItem(args),
                new ExcludeCommandOptionItem(args),
                new OverrideCommandOptionItem(args),
            };
        }
    }
}