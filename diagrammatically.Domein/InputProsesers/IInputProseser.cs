using System.Collections.Generic;

namespace diagrammatically.Domein.InputProsesers
{
    public interface IInputProseser
    {
        void Loockup(string filter, string source, IEnumerable<string> langs);
    }
}