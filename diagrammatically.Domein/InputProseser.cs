using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diagrammatically.Domein
{
    public class InputProseser
    {
        private readonly IEnumerable<InputConsumer> _inputConsumers;

        public InputProseser(IEnumerable<InputConsumer> inputConsumers)
        {
            _inputConsumers = inputConsumers;
        }

        public IEnumerable<IEnumerable<string>> Loockup(string input)
        {
            var tasks = _inputConsumers
                .Select(consumer => consumer.Consume(input))
                .ToArray();
            //.Select(async task => await task);
            Task.WaitAll(tasks);
            return tasks.Select(t => t.Result);
        }
    }
}
