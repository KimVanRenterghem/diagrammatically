using System.Collections.Generic;

namespace diagrammatically.Domein.Interfaces
{
    public interface IOptionConsumer
    {
        void Consume(string filter, string source, IEnumerable<WordMatch> matches);
    }
}
