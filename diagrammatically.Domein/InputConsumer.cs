using System.Collections.Generic;
using System.Threading.Tasks;

namespace diagrammatically.Domein
{
    public interface InputConsumer
    {
        Task<IEnumerable<string>> Consume(string input);
    }
}
