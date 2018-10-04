using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein.InputProsesers
{
    public class CreateWordInPut : InputProseserStream
    {
        private readonly IWordsSplitter _wordsSplitter;
        private readonly IWordsSplitter _sentensSplitter;
        private Subscriber<string> _subscriber;

        public CreateWordInPut( IWordsSplitter wordsSplitter, IWordsSplitter sentensSplitter)
        {
            _wordsSplitter = wordsSplitter;
            _sentensSplitter = sentensSplitter;
        }

        public void Lisen(string filter, string source, IEnumerable<string> langs)
        {
            var f = filter;
            var sentenses = _sentensSplitter.Split(filter);
            filter = sentenses.Last();

            filter = _wordsSplitter
                .Split(filter)
                .Last();

            _subscriber.Lisen(filter, source, langs);
        }

        public void Subscribe(Subscriber<string> subscriber)
        {
            _subscriber = subscriber;
        }
    }
}