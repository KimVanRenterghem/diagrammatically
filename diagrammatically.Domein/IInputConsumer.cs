using System.Collections.Generic;
using System.Threading.Tasks;

namespace diagrammatically.Domein
{
    public interface IInputConsumer
    {
        Task<IEnumerable<WordMatch>> ConsumeAsync(string input, IEnumerable<string> langs);
    }
}
