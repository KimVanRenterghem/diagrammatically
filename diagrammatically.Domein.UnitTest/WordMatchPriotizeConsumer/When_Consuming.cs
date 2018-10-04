using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.WordMatchConsumer;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordMatchPriotizeConsumer
{
    public class When_Consuming : Given_When_Then
    {
        private WordMatchConsumer.WordMatchPriotizeConsumer _sub;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var optionconsumerMock = new Mock<IWordMatchConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Lisen(It.IsAny<IEnumerable<WordMatch>>(), "vs code", new[] { "be" }))
                .Callback<IEnumerable<WordMatch>, string, IEnumerable<string>>((matches, source, langs) => _matshes.Add(matches));

            _sub = new WordMatchConsumer.WordMatchPriotizeConsumer(8);
            _sub.Subscribe(optionconsumerMock.Object);
        }

        protected override void When()
        {

            const string filter = "word";

            _sub.Lisen(
                new[]
                {
                    new WordMatch(filter, "wordis", 0.5, 0, "google"),
                    new WordMatch(filter, "worden", 1, 0, "localdb"),
                    new WordMatch(filter, "worden", 0.3, 0, "google"),
                    new WordMatch(filter, "word", 1, 0, "localdb")
                },
                "vs code",
                new[] { "be" });
        }

        [Fact]
        public void Then_The_List_Is_Orderd()
        {
            const string filter = "word";

            _matshes.First().Should().BeEquivalentTo(
                new WordMatch(filter, "word", 1, 0, "localdb"),
                new WordMatch(filter, "worden", 1, 0, "localdb"),
                new WordMatch(filter, "wordis", 0.5, 0, "google"),
                new WordMatch(filter, "worden", 0.3, 0, "google")
                );
        }
    }
}