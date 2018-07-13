using System.Collections.Generic;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public interface IWordMatchConsumerConsumer
    {
        void Consume(string filter, string source, IEnumerable<WordMatch> matches);
    }
}
