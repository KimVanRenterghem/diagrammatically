using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchPriotizeConsumer : IWordMatchConsumerConsumer
    {
        private readonly IWordMatchConsumerConsumer _optionconsumer;

        public WordMatchPriotizeConsumer(IWordMatchConsumerConsumer optionconsumer)
        {
            _optionconsumer = optionconsumer;
        }

        public void Consume(string filter, string source, IEnumerable<WordMatch> matches)
        {
            matches = matches
                .OrderByDescending(wordMatch => wordMatch.Match)
                .ThenBy(wordMatch => wordMatch.Word.Length)
                .Take(10)
                .ToArray();
            _optionconsumer.Consume(filter, source, matches);
        }
    }
}