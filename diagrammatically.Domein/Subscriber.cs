using System.Collections.Generic;

namespace diagrammatically.Domein
{
    public interface Subscriber<in TMessage>
    {
        void Lisen(TMessage message, string source, IEnumerable<string> langs);
    }
}