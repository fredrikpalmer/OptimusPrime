using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace OptimusPrime.Tools.ImportandExportSettings
{
    [RunInstaller(true)]
    public class InstallerClass : Installer
    {
        public InstallerClass()
        {
            // Attach the 'Committed' event.
            this.Committed += MyInstaller_Committed;
            // Attach the 'Committing' event.
            this.Committing += MyInstaller_Committing;
        }

        // Event handler for 'Committing' event.
        private void MyInstaller_Committing(object sender, InstallEventArgs e)
        {
            //Console.WriteLine("");
            Console.WriteLine("Committing Event occurred.");
            //Console.WriteLine("");
        }

        // Event handler for 'Committed' event.
        private void MyInstaller_Committed(object sender, InstallEventArgs e)
        {
            try
            {

                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Directory.SetCurrentDirectory(directoryName);

                var processStartInfo = new ProcessStartInfo(directoryName + "\\OptimusPrime.Tools.ImportandExportSettings.exe")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };

                Process.Start(processStartInfo);
            }
            catch
            {
                // Do nothing... 
            }
        }

        // Override the 'Install' method.
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }
    }
}
