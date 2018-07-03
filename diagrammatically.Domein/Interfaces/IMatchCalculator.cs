namespace diagrammatically.Domein.Interfaces
{
    public interface IMatchCalculator
    {
        double Calculate(string filter, string match);
        char Replace(char toReplace);
    }
}