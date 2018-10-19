using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchPriotizeConsumer : IWordMatchConsumer, Subscriber<WordSelection>
    {
        private readonly List<Subscriber<IEnumerable<WordMatch>>> _optionconsumer = new List<Subscriber<IEnumerable<WordMatch>>>();
        private readonly int _lenth;

        public WordMatchPriotizeConsumer(int lenth)
        {
            _lenth = lenth;
        }

        public void Lisen(IEnumerable<WordMatch> matches, string source, IEnumerable<string> langs)
        {
            matches = matches
                .OrderByDescending(wordMatch => wordMatch.Match)
                .ThenBy(wordMatch => wordMatch.Word.Length)
                .Take(_lenth)
                .ToArray();

            PublichMatches(matches, source, langs);
        }

        private void PublichMatches(IEnumerable<WordMatch> matches, string source, IEnumerable<string> langs)
        {
            _optionconsumer
                .ForEach(op => op.Lisen(matches, source, langs));
        }

        public void Subscribe(Subscriber<IEnumerable<WordMatch>> subscriber)
        {
            _optionconsumer.Add(subscriber);
        }

        public void Lisen(WordSelection message, string source, IEnumerable<string> langs)
            => PublichMatches(new WordMatch[0], source, langs);
    }
}