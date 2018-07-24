using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchPriotizeConsumer : IWordMatchConsumer
    {
        private readonly IEnumerable<IWordMatchConsumer> _optionconsumer;
        private readonly int _lenth;

        public WordMatchPriotizeConsumer(IEnumerable<IWordMatchConsumer> optionconsumer, int lenth)
        {
            _optionconsumer = optionconsumer;
            _lenth = lenth;
        }

        public void Consume(string filter, string source, IEnumerable<WordMatch> matches)
        {
            matches = matches
                .OrderByDescending(wordMatch => wordMatch.Match)
                .ThenBy(wordMatch => wordMatch.Word.Length)
                .Take(_lenth)
                .ToArray();

            _optionconsumer
                .ForEach(op => op.Consume(filter, source, matches));
        }
    }
}