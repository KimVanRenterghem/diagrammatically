using System.Collections.Generic;
using diagrammatically.Domein;

namespace diagrammatically.electron_edge.api
{
    public class WordMatchesConsumer : Subscriber<IEnumerable<WordMatch>>
    {
        public void Lisen(IEnumerable<WordMatch> matches, string source, IEnumerable<string> langs)
            => OptionApi._LastMatches = matches;
    }
}