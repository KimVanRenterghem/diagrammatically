using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;
using diagrammatically.Domein;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.AvaloniaUi
{
    public class OptionsConsumer : IOptionConsumer
    {
        private readonly Action<IEnumerable<Option>> _setOptions;

        public OptionsConsumer(Action<IEnumerable<Option>> setOptions)
        {
            _setOptions = setOptions;

        }

        public void Consume(IEnumerable<WordMatch> wordMatches)
            => wordMatches
                .OrderByDescending(wordMatch => wordMatch.Match)
                .ThenBy(wordMatch => wordMatch.Word.Length)
                .Take(10)
                .Select(wordMatch
                    => new Option
                    {
                        Word = wordMatch.Word
                    })
                .Pipe(_setOptions);
    }
}
