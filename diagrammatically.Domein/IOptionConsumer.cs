using System.Collections.Generic;

namespace diagrammatically.Domein
{
    public interface IOptionConsumer
    {
        void Consume(IEnumerable<string> options);
    }
}
