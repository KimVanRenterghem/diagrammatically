using System.Collections.Generic;
using diagrammatically.Domein;
using FluentAssertions;
using Xunit;

namespace diagrammatically.localDictionary.ItgTest.Reposetry
{
    public class When_Searching_A_Part_Of_The_Word : Given_When_Then
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
                    Word = "test",
                    Lang = "nl"
                },
                Used = 0
            });
            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "word",
                    Lang = "nl"
                },
                Used = 0
            });
        }

        protected override void When()
        {
            _res = _sut.Get("te", new[] { "nl" });
        }

        [Fact]
        public void Then_The_word_is_returned()
        {
            _res.Should().BeEquivalentTo(new Word()
            {
                Id = new WordKey
                {
                    Word = "test",
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