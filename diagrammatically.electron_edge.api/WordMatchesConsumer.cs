using System.Collections.Generic;
using diagrammatically.Domein;
using diagrammatically.Domein.WordMatchConsumer;

namespace diagrammatically.electron_edge.api
{
    public class WordMatchesConsumer : IWordMatchConsumer
    {
        public void Consume(string filter, string source, IEnumerable<WordMatch> matches)
            => OptionApi._LastMatches = matches;
    }
}