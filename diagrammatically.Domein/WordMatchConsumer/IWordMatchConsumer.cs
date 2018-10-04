using System.Collections.Generic;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public interface IWordMatchConsumer : Subscriber<IEnumerable<WordMatch>> , Publisher<IEnumerable<WordMatch>>
    {
    }
}
