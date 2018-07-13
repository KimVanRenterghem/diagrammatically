using System.Threading.Tasks;
using diagrammatically.Domein;

namespace diagrammatically.electron_edge.api
{
    public class OptionApi
    {
        public async Task<object> GetCurrentOptions(dynamic input)
        {
            return new []
            {
                new WordMatch("st","stoel",0.3,6,"localdb"),
                new WordMatch("st","steen",0.3,6,"localdb"),
                new WordMatch("st","schommel",0.1,6,"localdb"),
            };
        }
    }
}