using System.Collections.Generic;

namespace diagrammatically.Domein.Interfaces
{
    public interface IWordsSplitter
    {
        IEnumerable<string> Split(string value);
    }
}