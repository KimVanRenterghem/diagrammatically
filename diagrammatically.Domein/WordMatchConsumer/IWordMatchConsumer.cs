using System.Collections.Generic;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public interface IWordMatchConsumer
    {
        void Consume(string filter, string source, IEnumerable<WordMatch> matches);
    }
}
