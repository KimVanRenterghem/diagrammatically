using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IEnumerable<IEnumerable<string>> Loockup(string input)
        {
            var tasks = _inputConsumers
                .Select(consumer =>
                Task.Run(async () =>
                    {
                        var options = await consumer.Consume(input);
                        Consume(options);
                        return options;
                    })
                ).ToArray();

            Task.WaitAll(tasks);
            return tasks.Select(t => t.Result);
        }

        private void Consume(IEnumerable<string> options)
            => _optionConsumers
                .ForEach(optionConsumer => optionConsumer.Consume(options));

    }
}
