using System.Diagnostics;
using OptimusPrime.Cli.Helpers;

namespace OptimusPrime.Cli.Commands
{
    public class DefaultProcessor : IProcessor
    {
        private readonly ILogger _logger;

        public DefaultProcessor(ILogger logger)
        {
            _logger = logger;
        }

        public void Start(string fileName, string arguments)
        {
            var startInfo = new ProcessStartInfo(fileName, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(startInfo))
            {
                process.OutputDataReceived += Process_OutputDataReceived;
                process.BeginOutputReadLine();

                process.ErrorDataReceived += Process_ErrorDataReceived;
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;

            _logger.Error(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;

            _logger.Info(e.Data);
        }
    }
}