using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein.WordMatchConsumer
{
    public class WordMatchPriotizeConsumer : IWordMatchConsumer
    {
        private readonly List<Subscriber<IEnumerable<WordMatch>>>_optionconsumer = new List<Subscriber<IEnumerable<WordMatch>>>();
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

            _optionconsumer
                .ForEach(op => op.Lisen(matches, source, langs));
        }

        public void Subscribe(Subscriber<IEnumerable<WordMatch>> subscriber)
        {
            _optionconsumer.Add(subscriber);
        }
    }
}