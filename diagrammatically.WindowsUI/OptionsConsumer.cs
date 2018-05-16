using System;
using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein;

namespace diagrammatically.WindowsUI
{
    public class OptionsConsumer : IOptionConsumer
    {
        private readonly Action<IEnumerable<Option>> SetOptions;

        public OptionsConsumer(Action<IEnumerable<Option>> setOptions)
        {
            SetOptions = setOptions;

        }

        public void Consume(IEnumerable<WordMatch> options)
            => SetOptions(
                options
                    .Select(word
                        => new Option
                        {
                            Word = word.Word
                        }));
    }
}
