using System.Collections.Generic;
using diagrammatically.Domein;
using FluentAssertions;
using Xunit;

namespace diagrammatically.localDictionary.ItgTest.Reposetry
{
    public class When_Searching_A_Exact_Match : Given_When_Then
    {
        private localDictionary.Reposetry _sut;
        private IEnumerable<Word> _res;

        protected override void Given()
        {
            _sut = new localDictionary.Reposetry(new MatchCalculator());

            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "lokaal",
                    Lang = "nl"
                },
                Used = 0
            });
            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "loket",
                    Lang = "nl"
                },
                Used = 0
            });
            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "laken",
                    Lang = "nl"
                },
                Used = 0
            });
        }

        protected override void When()
        {
            _res = _sut.Get("lok", new[] { "nl" });
        }

        [Fact]
        public void Then_The_word_is_returned()
        {
            _res.Should().BeEquivalentTo(
                new Word()
                {
                    Id = new WordKey
                    {
                        Word = "lokaal",
                        Lang = "nl"
                    },
                    Used = 0
                },
                new Word()
                {
                    Id = new WordKey
                    {
                        Word = "loket",
                        Lang = "nl"
                    },
                    Used = 0
                });
        }

        public override void Dispose()
        {
            _sut.Drop();
        }
    }
}