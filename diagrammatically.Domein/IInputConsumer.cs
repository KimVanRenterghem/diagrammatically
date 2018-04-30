using System.Collections.Generic;
using System.Threading.Tasks;

namespace diagrammatically.Domein
{
    public interface IInputConsumer
    {
        Task<IEnumerable<string>> Consume(string input);
    }
}
