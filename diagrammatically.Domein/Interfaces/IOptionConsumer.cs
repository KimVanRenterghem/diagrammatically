using System.Collections.Generic;

namespace diagrammatically.Domein.Interfaces
{
    public interface IOptionConsumer
    {
        void Consume(IEnumerable<WordMatch> wordMatches);
    }
}
