using System;
using System.Text;
using OptimusPrime.Cli.Models;

namespace OptimusPrime.Cli.Helpers
{
    internal class MsBuildCommandBuilder
    {
        private string _msBuildFileName;
        private string _targetName;
        private FileInformation _configFileInfo;

        public MsBuildCommandBuilder WithMsBuildFileName(string fileName)
        {
            _msBuildFileName = fileName;
            return this;
        }

        public MsBuildCommandBuilder WithTargetName(string targetName)
        {
            _targetName = targetName;
            return this;
        }

        public MsBuildCommandBuilder WithFileInfo(FileInformation configFileInfo)
        {
            _configFileInfo = configFileInfo;
            return this;
        }

        public string Build()
        {
            var sb = new StringBuilder();
            sb.Append($"{_msBuildFileName} ");
            sb.Append($"/t:{_targetName} ");

            sb.Append($"/p:SourceFile={Uri.EscapeDataString(_configFileInfo.SourceFile)} ");
            sb.Append($"/p:TransformFile={Uri.EscapeDataString(_configFileInfo.TransformFile)} ");
            sb.Append($"/p:DestinationFile={Uri.EscapeDataString(_configFileInfo.DestinationFile)} ");

            return sb.ToString();
        }
    }
}