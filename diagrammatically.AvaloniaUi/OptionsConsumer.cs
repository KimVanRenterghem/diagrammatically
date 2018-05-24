using System;
using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein;

namespace diagrammatically.AvaloniaUi
{
    public class OptionsConsumer : IOptionConsumer
    {
        private readonly Action<IEnumerable<Option>> SetOptions;

        public OptionsConsumer(Action<IEnumerable<Option>> setOptions)
        {
            SetOptions = setOptions;

        }

        public void Consume(IEnumerable<string> options)
            => SetOptions(
                options
                    .Select(o
                        => new Option
                        {
                            Word = o
                        }));
    }
}
