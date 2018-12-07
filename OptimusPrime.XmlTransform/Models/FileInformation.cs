namespace OptimusPrime.Cli.Models
{
    public class FileInformation
    {
        public string SourceFile { get; set; }
        public string TransformFile { get; set; }
        public string DestinationFile => SourceFile;
        public bool IsOverride { get; set; }
    }
}
