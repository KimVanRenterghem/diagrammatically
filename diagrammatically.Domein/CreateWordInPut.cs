using System;
using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public class CreateWordInPut : IInputProseser
    {
        private readonly IInputProseser _inputProseser;
        private readonly char[] _wordSpliters = " _\n\t".ToCharArray();
        private readonly char[] _sentensSpliters = ".,!?:;()&|".ToCharArray();
        private readonly char[] _uppers = "ABCDEFGHIJKLMNOPQRSTUVWYYZ".ToCharArray();

        public CreateWordInPut(IInputProseser inputProseser)
        {
            _inputProseser = inputProseser;
        }

        public void Loockup(string filter, IEnumerable<string> langs)
        {
            var sentenses = SplitInWords(filter, _sentensSpliters, new char[0]);
            filter = sentenses.Last();

            var words = SplitInWords(filter, _wordSpliters, _uppers);

            filter = words.Last();
            _inputProseser.Loockup(filter, langs);
        }

        private IEnumerable<string> SplitInWords(string value, IEnumerable<char> invisebelSpliters, IEnumerable<char> visebelSpliters)
        {
            //export this to diff class wit thests for
            // no match
            // 1 match
            // multiple matches
            // multiple mixed matches
            // visebel
            // unviseble

            Func<string, string> TrimSplitters(char[] invisebel)
            {
                return word => word.IndexOfAny(invisebel) >= 0
                    ? invisebel.Aggregate(word, (w, r) => w.Replace(r.ToString(), ""))
                    : word;
            }

            Func<(int index, int length), string> Split(string s)
            {
                return split =>
                    s.Substring(split.index, split.length);
            }

            int GetLenth(int last, int index)
            {
                return last - index;
            }

            var invisebelSplitersArray = invisebelSpliters.ToArray();

            var res = invisebelSplitersArray
                .Union(visebelSpliters)
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

                        return new
                        {
                            ag.splits,
                            lastIndex = index
                        };
                    });
            if (res.lastIndex > -1)
                res.splits.Add((res.lastIndex, GetLenth(value.Length, res.lastIndex)));

            var words = res.splits
                .Select(Split(value))
                .Select(TrimSplitters(invisebelSplitersArray))
                .ToArray();
            return words.Any() ? words : new[] { value };
        }
    }
}