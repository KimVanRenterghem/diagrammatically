using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;
using diagrammatically.Domein;

namespace diagrammatically.AvaloniaUi
{
    public class WordMatchesConsumer : Subscriber<IEnumerable<WordMatch>>
    {
        private readonly Action<IEnumerable<Option>> _setOptions;

        public WordMatchesConsumer(Action<IEnumerable<Option>> setOptions)
        {
            _setOptions = setOptions;

        }

        public void Lisen(IEnumerable<WordMatch> wordMatches, string source, IEnumerable<string> langs)
            => wordMatches
                .Select(wordMatch
                    => new Option
                    {
                        Word = wordMatch.Word
                    })
                .Pipe(_setOptions);
    }
}
