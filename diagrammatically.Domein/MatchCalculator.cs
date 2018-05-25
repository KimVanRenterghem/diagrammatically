using System.Linq;

namespace diagrammatically.Domein
{
    public class MatchCalculator : IMatchCalculator
    {
        public double Calculate(string filter, string match)
            => filter
                   .Where((letter, index) => match[index] == letter)
                   .Count() / (double)match.Length;
    }
}