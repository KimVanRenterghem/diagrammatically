using System;
using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein;

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
            => _setOptions(
                wordMatches
                    .Select(wordMatch
                        => new Option
                        {
                            Word = wordMatch.Word
                        }));
    }
}
