namespace OptimusPrime.Cli.Constants
{
    internal static class ApplicationConstants
    {
        internal static class Commands
        {
            internal static class Transform
            {
                public const string Argument = "transform";
            }
        }

        internal static class Options
        {
            internal static class Path
            {
                public const string Argument = "--path";
                public const string ShortArgument = "-p";
                public const string Description = "Root path of the project";
            }

            internal static class Configuration
            {
                public const string Argument = "--environment";
                public const string ShortArgument = "-e";
                public const string Description = "Transform for this environment";

            }

            internal static class Exclude
            {
                public const string Argument = "--exclude";
                public const string Description = "Exclude files from transformation";
            }

            internal static class Override
            {
                public const string Argument = "--override";
                public const string Description = "Override certain config elements";
            }
        }
    }
}
