using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.WordMatchConsumer;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordMatchPriotizeConsumer
{
    public class Given_Limiting_Lenth_When_Consuming : Given_When_Then
    {
        private WordMatchConsumer.WordMatchPriotizeConsumer _sub;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var optionconsumerMock1 = new Mock<IWordMatchConsumer>();
            optionconsumerMock1
                .Setup(optionconsumer => optionconsumer.Consume("word", "vs code", It.IsAny<IEnumerable<WordMatch>>()))
                .Callback<string, string, IEnumerable<WordMatch>>((filter, source, matches) => _matshes.Add(matches));

            _sub = new WordMatchConsumer.WordMatchPriotizeConsumer(new[] { optionconsumerMock1.Object },2);
        }

        protected override void When()
        {
            const string filter = "word";

            _sub.Consume(filter, "vs code", new[]
            {
                new WordMatch(filter, "wordis", 0.5, 0, "google"),
                new WordMatch(filter, "worden", 1, 0, "localdb"),
                new WordMatch(filter, "worden", 0.3, 0, "google"),
                new WordMatch(filter, "word", 1, 0, "localdb")
            });
        }

        [Fact]
        public void Then_The_List_Is_Limited_To_Twoo()
        {
            const string filter = "word";

            _matshes.First().Should().BeEquivalentTo(
                new WordMatch(filter, "word", 1, 0, "localdb"),
                new WordMatch(filter, "worden", 1, 0, "localdb")
            );
        }
    }
}