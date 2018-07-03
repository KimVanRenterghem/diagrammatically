using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Pipe;
using diagrammatically.Domein.Interfaces;

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
            var seed = (new List<(int index, int length)>(), -1);

            var (splits, lastIndex) = _invisebleSplitters
                .Union(_visebelSplitters)
                .SelectMany(value.AllIndexesOf)
                .OrderBy(i => i)
                .Aggregate(seed, GetSplits);

            if (lastIndex != -1)
                splits.Add((lastIndex, GetLenth(value.Length, lastIndex)));

            var words = splits
                .Select(Substring(value))
                .Select(TrimSplitters(_invisebleSplitters))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            return words.Any() ?
                words :
                new[] { value }
                    .Select(TrimSplitters(_invisebleSplitters))
                    .ToArray();
        }

        private (List<(int index, int length)> splits, int lastIndex) GetSplits((List<(int index, int length)> splits, int lastIndex) ag, int index)
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

            return (ag.splits, index);
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