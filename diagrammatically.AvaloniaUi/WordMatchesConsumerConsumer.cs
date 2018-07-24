using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;
using diagrammatically.Domein;
using diagrammatically.Domein.WordMatchConsumer;

namespace diagrammatically.AvaloniaUi
{
    public class WordMatchesConsumer : IWordMatchConsumer
    {
        private readonly Action<IEnumerable<Option>> _setOptions;

        public WordMatchesConsumer(Action<IEnumerable<Option>> setOptions)
        {
            _setOptions = setOptions;

        }

        public void Consume(string filter, string source, IEnumerable<WordMatch> wordMatches)
            => wordMatches
                .Select(wordMatch
                    => new Option
                    {
                        Word = wordMatch.Word
                    })
                .Pipe(_setOptions);
    }
}
