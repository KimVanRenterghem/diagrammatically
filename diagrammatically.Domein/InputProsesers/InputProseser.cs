using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;
using diagrammatically.Domein.WordMatchConsumer;

namespace diagrammatically.Domein.InputProsesers
{
    public class InputProseser : IInputProseser
    {
        private readonly IEnumerable<IInputConsumer> _inputConsumers;
        private readonly IEnumerable<IWordMatchConsumerConsumer> _optionConsumers;

        public InputProseser(IEnumerable<IInputConsumer> inputConsumers, IEnumerable<IWordMatchConsumerConsumer> optionConsumers)
        {
            _inputConsumers = inputConsumers;
            _optionConsumers = optionConsumers;
        }

        public void Loockup(string filter, string source, IEnumerable<string> langs)
        {
            Action<IEnumerable<WordMatch>> BroadCastWithFilterAndSource(string f, string s)
            {
                return matches => BroadCast(f, s, matches);
            }

            if (string.IsNullOrEmpty(filter))
            {
                BroadCastWithFilterAndSource(filter, source)(new WordMatch[0]);
                return;
            }
            
            _inputConsumers
                .Select(async consumer
                    => (await consumer.ConsumeAsync(filter, langs))
                        .Pipe(BroadCastWithFilterAndSource(filter, source)))
                .ToArray()
                .Pipe(Task.WhenAll);
        }

        private void BroadCast(string filter, string source, IEnumerable<WordMatch> matches)
            => _optionConsumers
                .ForEach(optionConsumer => optionConsumer.Consume(filter, source, matches));
    }
}
