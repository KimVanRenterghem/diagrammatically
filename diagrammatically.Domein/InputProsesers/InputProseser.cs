using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein.InputProsesers
{
    public class InputProseser : Subscriber<string>, Publisher<IEnumerable<WordMatch>>
    {
        private readonly IEnumerable<WordFinder> _wordFinders;
        private readonly List<Subscriber<IEnumerable<WordMatch>>> _wordMatchLiseners = new List<Subscriber<IEnumerable<WordMatch>>>();

        public InputProseser(IEnumerable<WordFinder> wordFinders)
        {
            _wordFinders = wordFinders;
        }

        public void Lisen(string filter, string source, IEnumerable<string> langs)
        {
            if (string.IsNullOrEmpty(filter))
            {
                BroadCastWithFilterAndSource(source, langs)(new WordMatch[0]);
                return;
            }

            _wordFinders
                .Select(async consumer
                    => (await consumer.ConsumeAsync(filter, langs))
                        .Pipe(BroadCastWithFilterAndSource(source,langs)))
                .ToArray()
                .Pipe(Task.WhenAll);

            Action<IEnumerable<WordMatch>> BroadCastWithFilterAndSource(string s, IEnumerable<string> l)
            {
                return matches => BroadCast(matches, s, l);
            }
        }

        private void BroadCast(IEnumerable<WordMatch> matches, string source, IEnumerable<string> langs)
            => _wordMatchLiseners
                .ForEach(optionConsumer => optionConsumer.Lisen(matches, source, langs));

        public void Subscribe(Subscriber<IEnumerable<WordMatch>> subscriber)
        {
            _wordMatchLiseners.Add(subscriber);
        }
    }
}
