using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using OptimusPrime.Cli.Commands;
using OptimusPrime.Cli.Commands.Options;
using OptimusPrime.Cli.Config;
using OptimusPrime.Cli.Helpers;
using OptimusPrime.Cli.Models;

namespace OptimusPrime.Cli.Tests
{
    [TestFixture]
    public class MainTests
    {
        private Mock<ILogger> _loggerMock;
        private Mock<IMsBuildFileInfoService> _fileInfoServiceMock;
        private Mock<IProcessor> _processorMock;
        private Mock<IApplicationConfiguration> _configurationMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger>();

            _configurationMock = new Mock<IApplicationConfiguration>();
            _configurationMock.Setup(x => x.GetValue("MSBuildPath")).Returns("msbuild.exe");
            _configurationMock.Setup(x => x.GetValue("MSBuildFileName")).Returns("optimusprime.targets");
            _configurationMock.Setup(x => x.GetValue("MSBuildTargetName")).Returns("RunOptimusPrime");

            _fileInfoServiceMock = new Mock<IMsBuildFileInfoService>();
            _fileInfoServiceMock.Setup(x => x.GetAllFileInfo(It.IsAny<CommandOptions>())).Returns(GetAllFileInfo());

            _processorMock = new Mock<IProcessor>();
        }

        [Test]
        public void ItShouldExecuteMsBuildTransformCommandWithExpectedParameters()
        {
            var actualExecutions = new List<string>();
            _processorMock.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((x, y) => actualExecutions.Add($"{x} {Uri.UnescapeDataString(y)}"));

            Program.InitializerAction = ctx =>
            {
                ctx.UseLogger(_loggerMock.Object);
                ctx.UseConfiguration(_configurationMock.Object);
                ctx.UseFileInfoService(_fileInfoServiceMock.Object);
                ctx.UseProcessor(_processorMock.Object);
            };

            Program.Main(new[]
            {
                "transform",
                "-p",
                @"C:\dev\repos\MyProject\",
                "-e",
                "Production",
                "--exclude",
                "custom.config",
                "--override"
            });

            var expectedExecutions = new List<string>
            {
                @"msbuild.exe optimusprime.targets /t:RunOptimusPrime /p:SourceFile=c:\dev\repos\MyProject\Web.config /p:TransformFile=c:\dev\repos\MyProject\Web.Production.config /p:DestinationFile=c:\dev\repos\MyProject\Web.config ",
                @"msbuild.exe optimusprime.targets /t:RunOptimusPrime /p:SourceFile=c:\dev\repos\MyProject\MySection.config /p:TransformFile=c:\dev\repos\MyProject\MySection.Production.config /p:DestinationFile=c:\dev\repos\MyProject\MySection.config ",
            };

            CollectionAssert.AreEquivalent(expectedExecutions, actualExecutions);
        }

        private IEnumerable<FileInformation> GetAllFileInfo()
        {
            yield return new FileInformation
            {
                SourceFile = @"c:\dev\repos\MyProject\MySection.config",
                TransformFile = @"c:\dev\repos\MyProject\MySection.Production.config"
            };
            yield return new FileInformation
            {
                SourceFile = @"c:\dev\repos\MyProject\Web.config",
                TransformFile = @"c:\dev\repos\MyProject\Web.Production.config"
            };
        }
    }
}
