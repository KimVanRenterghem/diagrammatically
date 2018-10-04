using System.Collections.Generic;

namespace diagrammatically.Domein.InputProsesers
{
    public interface InputProseserStream : Subscriber<string>, Publisher<string>
    {
    }
}