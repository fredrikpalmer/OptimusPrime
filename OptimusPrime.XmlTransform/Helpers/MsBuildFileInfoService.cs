using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OptimusPrime.Cli.Commands.Options;
using OptimusPrime.Cli.Constants;
using OptimusPrime.Cli.Models;

namespace OptimusPrime.Cli.Helpers
{
    public class MsBuildFileInfoService : IMsBuildFileInfoService
    {
        public IEnumerable<FileInformation> GetAllFileInfo(CommandOptions commandOptions)
        {
            var path = commandOptions.GetArgumentValue<string>(ApplicationConstants.Options.Path.Argument);
            var environment = commandOptions.GetArgumentValue<string>(ApplicationConstants.Options.Configuration.Argument);
            var exclude = commandOptions.GetArgumentValue<string>(ApplicationConstants.Options.Exclude.Argument);
            var shouldUseOverride = commandOptions.GetArgumentValue<bool>(ApplicationConstants.Options.Override.Argument);

            var files = Directory.GetFiles(path, "*.config", SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                var directoryName = Path.GetDirectoryName(filePath) + "\\";
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var fileExtension = Path.GetExtension(filePath);

                if (filePath.Contains(@"\bin\")) continue;
                if (ShouldExcludeFileFromTransformation(exclude, fileName, fileExtension)) continue;
                if (!files.Any(x => CanTransformToEnvironment(x, $"{directoryName}{fileName}.{environment}{fileExtension}"))) continue;

                yield return new FileInformation
                {
                    SourceFile = $@"{CheckTrailingSlashOnRootDirectoryAndAddIfNotExists(path)}{GetFileNameRelativeToRootDirectory(path, directoryName, fileName)}{fileExtension}",
                    TransformFile = $@"{CheckTrailingSlashOnRootDirectoryAndAddIfNotExists(path)}{GetFileNameRelativeToRootDirectory(path, directoryName, fileName)}.{environment}{fileExtension}",
                };

                if (!shouldUseOverride) continue;
                if (!File.Exists($"{fileName}.{environment}{fileExtension}")) continue;

                yield return new FileInformation
                {
                    SourceFile = $@"{CheckTrailingSlashOnRootDirectoryAndAddIfNotExists(path)}{GetFileNameRelativeToRootDirectory(path, directoryName, fileName)}{fileExtension}",
                    TransformFile = $@"{Directory.GetCurrentDirectory()}\{fileName}.{environment}{fileExtension}",
                    IsOverride = true
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

        private static string GetFileNameRelativeToRootDirectory(string path, string directoryName, string fileName)
        {
            var directoryRelativeToProjectPath = GetDirectoryNameRelativeToRootDirectory(path, directoryName);
            return directoryRelativeToProjectPath == string.Empty
                ? fileName
                : $@"{directoryRelativeToProjectPath}\{fileName}";
        }

        private static string GetDirectoryNameRelativeToRootDirectory(string path, string directoryName)
        {
            return directoryName?.Replace(path, "").TrimEnd('\\');
        }

        private static string CheckTrailingSlashOnRootDirectoryAndAddIfNotExists(string path)
        {
            return path.EndsWith("\\")
                ? path
                : $"{path}\\";
        }
    }
}