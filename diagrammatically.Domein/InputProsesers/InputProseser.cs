using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein.InputProsesers
{
    public class InputProseser : IInputProseser
    {
        private readonly IEnumerable<IInputConsumer> _inputConsumers;
        private readonly IEnumerable<IOptionConsumer> _optionConsumers;

        public InputProseser(IEnumerable<IInputConsumer> inputConsumers, IEnumerable<IOptionConsumer> optionConsumers)
        {
            _inputConsumers = inputConsumers;
            _optionConsumers = optionConsumers;
        }

        public void Loockup(string filter, string source, IEnumerable<string> langs)
        {
            if (string.IsNullOrEmpty(filter))
                return;

            _inputConsumers
                .Select(async consumer
                    => (await consumer.ConsumeAsync(filter, langs))
                        .Pipe(BroadCast))
                .ToArray()
                .Pipe(Task.WhenAll);
        }

        private void BroadCast(IEnumerable<WordMatch> matches)
            => _optionConsumers
                .ForEach(optionConsumer => optionConsumer.Consume(matches));
    }
}
