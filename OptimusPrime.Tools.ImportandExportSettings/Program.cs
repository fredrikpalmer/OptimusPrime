using System;
using System.IO;

namespace OptimusPrime.Tools.ImportandExportSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dteType = Type.GetTypeFromProgID("VisualStudio.DTE.15.0", true);
                var dte = (EnvDTE.DTE)Activator.CreateInstance(dteType);

                var commandArgs = $@"/import:""{AppDomain.CurrentDomain.BaseDirectory}OptimusPrime.vssettings""";
                dte.ExecuteCommand("Tools.ImportandExportSettings", commandArgs);
            }
            catch (Exception exception)
            {
                using (var fs = File.Open("ImportAndExportSettings.txt", FileMode.OpenOrCreate, FileAccess.Write))
                using (var streamWriter = new StreamWriter(fs))
                {
                    streamWriter.WriteLine($"Import external tools failed: {exception}");
                }
            }
        }
    }
}
