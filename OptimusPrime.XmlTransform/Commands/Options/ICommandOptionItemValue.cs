namespace OptimusPrime.Cli.Commands.Options
{
    public interface ICommandOptionItemValue<out T>
    {
        T GetValue();
    }
}