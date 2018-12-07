namespace OptimusPrime.Cli.Commands
{
    public abstract class Command
    {
        public string Name { get; }
        public string Description { get; }

        protected Command(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Execute();
        public abstract void PrintUsage();
    }
}