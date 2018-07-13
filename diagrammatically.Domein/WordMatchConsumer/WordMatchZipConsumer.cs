using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchZipConsumerConsumer : IWordMatchConsumerConsumer
    {
        private readonly IWordMatchConsumerConsumer _wordMatchConsumerConsumer;
        private string _filter;
        private string _source;
        private IEnumerable<WordMatch> _wordMatches;

        public WordMatchZipConsumerConsumer(IWordMatchConsumerConsumer wordMatchConsumerConsumer)
        {
            _wordMatchConsumerConsumer = wordMatchConsumerConsumer;
        }
        public void Consume(string filter, string source, IEnumerable<WordMatch> wordMatches)
        {
            if (_filter == filter && _source == source)
            {
                _wordMatches = wordMatches
                    .Where(wordMatch => _wordMatches.All(w => w.Word != wordMatch.Word))
                    .Pipe(_wordMatches.Union)
                    .ToList();
            }
            else
            {
                _filter = filter;
                _source = source;
                _wordMatches = wordMatches;
            }

            _wordMatchConsumerConsumer.Consume(filter, source, _wordMatches);
        }
    }
}