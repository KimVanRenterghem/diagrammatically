using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchZipConsumer : IWordMatchConsumer
    {
        private Subscriber<IEnumerable<WordMatch>> _wordMatchConsumer;
        private string _filter;
        private string _source;
        private IEnumerable<WordMatch> _wordMatches;
        
        public void Lisen(IEnumerable<WordMatch> wordMatches, string source, IEnumerable<string> langs) 
        {
            var filter = wordMatches.Any() ? wordMatches.First()?.Search : "";

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

            _wordMatchConsumer.Lisen(_wordMatches, source, langs);
        }

        public void Subscribe(Subscriber<IEnumerable<WordMatch>> subscriber)
        {
            _wordMatchConsumer = subscriber;
        }
    }
}