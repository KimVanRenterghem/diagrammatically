using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;

namespace diagrammatically.Domein
{
    public class InputProseser
    {
        private readonly IEnumerable<IInputConsumer> _inputConsumers;
        private readonly IEnumerable<IOptionConsumer> _optionConsumers;

        public InputProseser(IEnumerable<IInputConsumer> inputConsumers, IEnumerable<IOptionConsumer> optionConsumers)
        {
            _inputConsumers = inputConsumers;
            _optionConsumers = optionConsumers;
        }

        public void Loockup(string input)
            => _inputConsumers
                .Select(async consumer 
                    => (await consumer.Consume(input))
                        .Pipe(Consume))
                .ToArray()
                .Pipe(Task.WhenAll);

        private void Consume(IEnumerable<WordMatch> matches)
            => _optionConsumers
                .ForEach(optionConsumer => optionConsumer.Consume(matches));
    }
}
