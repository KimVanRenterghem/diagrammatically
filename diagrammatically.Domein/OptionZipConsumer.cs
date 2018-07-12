using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein
{
    public class OptionZipConsumer : IOptionConsumer
    {
        private readonly IOptionConsumer _optionConsumer;
        private string _filter;
        private string _source;
        private IEnumerable<WordMatch> _wordMatches;

        public OptionZipConsumer(IOptionConsumer optionConsumer)
        {
            _optionConsumer = optionConsumer;
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

            _optionConsumer.Consume(filter, source, _wordMatches);
        }
    }
}