using System;
using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public class WordsSplitter : IWordsSplitter
    {
        private readonly char[] _invisebleSplitters;
        private readonly char[] _visebelSplitters;

        public WordsSplitter(char[] invisebleSplitters, char[] visebelSplitters)
        {
            _invisebleSplitters = invisebleSplitters;
            _visebelSplitters = visebelSplitters;
        }

        public IEnumerable<string> Split(string value)
        {
            var res = _invisebleSplitters
                .Union(_visebelSplitters)
                .SelectMany(value.AllIndexesOf)
                .OrderBy(i => i)
                .Aggregate(
                    new
                    {
                        splits = new List<(int index, int length)>(),
                        lastIndex = -1
                    },
                    (ag, index) =>
                    {
                        if (ag.lastIndex != -1)
                        {
                            var lenth = GetLenth(index, ag.lastIndex);
                            ag.splits.Add((ag.lastIndex, lenth));
                        }
                        else
                        {
                            ag.splits.Add((0, index));
                        }

                        return new
                        {
                            ag.splits,
                            lastIndex = index
                        };
                    });
            if (res.lastIndex > -1)
                res.splits.Add((res.lastIndex, GetLenth(value.Length, res.lastIndex)));

            var words = res.splits
                .Select(Substring(value))
                .Select(TrimSplitters(_invisebleSplitters))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
            return words.Any() ? words : new[] { value };
        }

        private int GetLenth(int last, int index)
            => last - index;

        private Func<(int index, int length), string> Substring(string s)
        => split
            => s.Substring(split.index, split.length);
        private Func<string, string> TrimSplitters(char[] invisebel)
        => word
            => word.IndexOfAny(invisebel) >= 0
                ? invisebel.Aggregate(word, (w, r) => w.Replace(r.ToString(), ""))
                : word;
    }
}