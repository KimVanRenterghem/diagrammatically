using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public class MatchCalculator : IMatchCalculator
    {
        private Dictionary<char, char> replacers = new Dictionary<char, char>
        {
            {'á', 'a'},
            {'à', 'a'},
            {'ä', 'a'},
            {'â', 'a'},
            {'å', 'a'},
            {'ç', 'c'},
            {'é', 'e'},
            {'è', 'e'},
            {'ë', 'e'},
            {'ê', 'e'},
            {'í', 'i'},
            {'ì', 'i'},
            {'ï', 'i'},
            {'î', 'i'},
            {'ĳ', 'i'},
            {'ö', 'o'},
            {'ò', 'o'},
            {'ó', 'o'},
            {'ô', 'o'},
            {'ú', 'u'},
            {'ù', 'u'},
            {'ü', 'u'},
            {'û', 'u'},
        };

        public double Calculate(string filter, string match)
            => filter
                   .Where((letter, index) => match[index] == letter)
                   .Count() / (double)match.Length;

        public char Replace(char toReplace)
            => replacers.ContainsKey(toReplace) ? replacers[toReplace] : toReplace;
    }
}