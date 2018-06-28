using System.Collections.Generic;

namespace diagrammatically.Domein
{
    public interface IWordsSplitter
    {
        IEnumerable<string> Split(string value);
    }
}