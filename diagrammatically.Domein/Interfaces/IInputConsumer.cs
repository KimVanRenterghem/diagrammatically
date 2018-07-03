using System.Collections.Generic;
using System.Threading.Tasks;

namespace diagrammatically.Domein.Interfaces
{
    public interface IInputConsumer
    {
        Task<IEnumerable<WordMatch>> ConsumeAsync(string input, IEnumerable<string> langs);
    }
}
