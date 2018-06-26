using System;
using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public class WordSplitter : IInputProseser
    {
        private readonly IInputProseser _inputProseser;
        private readonly char[] _splits = " _".ToCharArray();
        private readonly char[] _kammels = "ABCDEFGHIJKLMNOPQRSTUVWYYZ".ToCharArray();

        public WordSplitter(IInputProseser inputProseser)
        {
            _inputProseser = inputProseser;
        }

        public void Loockup(string filter, IEnumerable<string> langs)
        {
            filter = filter
                .Split(_splits)
                .Last();

            var index = _kammels
                .Select(l => filter.LastIndexOf(l))
                .Where(i => i > 0)
                .OrderBy(i => i)
                .LastOrDefault();

            if (index > 0)
            {
                filter = filter.Substring(index);
            }
            _inputProseser.Loockup(filter, langs);
        }
    }
}