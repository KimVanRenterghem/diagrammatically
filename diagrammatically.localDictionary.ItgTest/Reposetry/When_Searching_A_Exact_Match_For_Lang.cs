using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace diagrammatically.localDictionary.ItgTest.Reposetry
{
    public class When_Searching_A_Exact_Match_For_Lang : Given_When_Then
    {
        private localDictionary.Reposetry _sut;
        private IEnumerable<Word> _res;

        protected override void Given()
        {
            _sut = new localDictionary.Reposetry();

            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "kasten",
                    Lang = "nl"
                },
                Used = 0
            });
            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "kasten",
                    Lang = "fr"
                },
                Used = 0
            });
            _sut.Add(new Word()
            {
                Id = new WordKey
                {
                    Word = "kasten",
                    Lang = "en"

                },
                Used = 0
            });
        }

        protected override void When()
        {
            _res = _sut.Get("kasten", new[] { "nl", "en" });
        }

        [Fact]
        public void Then_The_word_is_returned()
        {
            _res.Should().BeEquivalentTo(
                new Word()
                {
                    Id = new WordKey
                    {
                        Word = "kasten",
                        Lang = "nl"
                    },
                    Used = 0
                },
                new Word()
                {
                    Id = new WordKey
                    {
                        Word = "kasten",
                        Lang = "en"
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