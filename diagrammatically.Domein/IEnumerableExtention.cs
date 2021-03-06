﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public static class EnumerableExtention
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
            => list
                ?.ToList()
                .ForEach(action);
    }
}
