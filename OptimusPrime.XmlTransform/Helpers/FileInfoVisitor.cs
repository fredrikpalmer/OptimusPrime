using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OptimusPrime.Cli.Commands.Options;
using OptimusPrime.Cli.Models;
using static OptimusPrime.Cli.Constants.ApplicationConstants;

namespace OptimusPrime.Cli.Helpers
{
    public class FileInfoVisitor : IVisitor
    {
        private readonly ILogger _logger;

        public FileInfoVisitor(ILogger logger)
        {
            _logger = logger;
        }

        public IList<FileInformation> Files { get; private set; }

        public void Visit(IList<ICommandOptionItem> commandOptionItems)
        {
            var path = GetCommandOptionValue(commandOptionItems, CommandOptions.Path.Argument);
            var environment = GetCommandOptionValue(commandOptionItems, CommandOptions.Configuration.Argument);
            var exclude = GetCommandOptionValue(commandOptionItems, CommandOptions.Exclude.Argument);
            var shouldUseOverride = commandOptionItems.Any(x => x.MatchesArgument(CommandOptions.Override.Argument));

            Files = GetAllFileInfo(path, environment, exclude, shouldUseOverride).ToList();

            if (!Files.Any()) _logger.Error($"No files found in path: {path}");
        }

        private string GetCommandOptionValue(IList<ICommandOptionItem> commandOptionItems, string option)
        {
            return commandOptionItems.FirstOrDefault(x => x.MatchesArgument(option))?.GetValue();
        }

        private IEnumerable<FileInformation> GetAllFileInfo(string path, string environment, string exclude, bool shouldUseOverride)
        {
            var files = Directory.GetFiles(path, "*.config", SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                var directoryName = Path.GetDirectoryName(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var fileExtension = Path.GetExtension(filePath);

                if (filePath.Contains(@"\bin\")) continue;
                if (ShouldExcludeFileFromTransformation(exclude, fileName, fileExtension)) continue;
                if (!files.Any(x => CanTransformToEnvironment(x, $"{directoryName}\\{fileName}.{environment}{fileExtension}"))) continue;

                yield return new FileInformation
                {
                    FileName = GetFilePathRelativeToProjectPath(path, directoryName, fileName),
                    FileExtension = fileExtension,
                    DirectoryName = $@"{directoryName}\"
                };

                if (!shouldUseOverride) continue;
                if (!File.Exists($"{fileName}.{environment}{fileExtension}")) continue;

                yield return new FileInformation
                {
                    FileName = fileName,
                    FileExtension = fileExtension,
                    DirectoryName = $@"{Directory.GetCurrentDirectory()}\"
                };
            }
        }

        private bool ShouldExcludeFileFromTransformation(string exclude, string fileName, string fileExtension)
        {
            if (exclude == null) return false;

            return exclude.ToLower().Contains($"{fileName}{fileExtension}".ToLower());
        }

        private static bool CanTransformToEnvironment(string filePath, string filePathForEnvironment)
        {
            return filePath.Equals(filePathForEnvironment, StringComparison.OrdinalIgnoreCase);
        }

        private static string GetFilePathRelativeToProjectPath(string path, string directoryName, string fileName)
        {
            var directoryRelativeToProjectPath = GetDirectoryRelativeToProjectPath(path, directoryName);
            return directoryRelativeToProjectPath == string.Empty
                   ? fileName
                   : $"{directoryRelativeToProjectPath}\\{fileName}";
        }

        private static string GetDirectoryRelativeToProjectPath(string path, string directoryName)
        {
            var trimmedPath = GetPathWithoutTrailingSlash(path);
            return directoryName?.Replace(trimmedPath, "");
        }

        private static string GetPathWithoutTrailingSlash(string path)
        {
            return path?.TrimEnd('\\');
        }
    }
}