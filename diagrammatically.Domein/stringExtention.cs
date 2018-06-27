using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public static class stringExtention
    {
        public static IEnumerable<int> AllIndexesOf(this string value, char pathren)
            => value
                .Select((c, i) => c == pathren ? i : -1)
                .Where(i => i != -1)
                .ToArray();
    }
}