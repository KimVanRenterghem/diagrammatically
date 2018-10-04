using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.WordMatchConsumer;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordMatchZipConsumer
{
    public class When_Consuming : Given_When_Then
    {
        private WordMatchConsumer.WordMatchZipConsumer _sub;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var optionconsumerMock = new Mock<IWordMatchConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Lisen(It.IsAny<IEnumerable<WordMatch>>(), "vs code", new[] { "kim" }))
                .Callback<IEnumerable<WordMatch>, string, IEnumerable<string>>((matches, source, langs) => _matshes.Add(matches));

            _sub = new WordMatchConsumer.WordMatchZipConsumer();
            _sub.Subscribe(optionconsumerMock.Object);
        }

        protected override void When()
        {
            const string filter = "word";

            _sub.Lisen(
                new[]
                {
                    new WordMatch(filter, "worden", 1, 0, "localdb"),
                    new WordMatch(filter, "word", 0.1, 0, "localdb")
                },
                "vs code",
                new[] { "kim" });

            _sub.Lisen(
                new[]
                {
                    new WordMatch(filter, "wordis", 1, 0, "google") ,
                    new WordMatch(filter, "worden", 0.3, 0, "google")
                },
                "vs code",
                new[] { "kim" });
        }

        [Fact]
        public void Then_The_Lists_Are_Joined()
        {
            _matshes
                .Last()
                .Should().BeEquivalentTo(
                    new WordMatch("word", "worden", 1, 0, "localdb"),
                    new WordMatch("word", "word", 0.1, 0, "localdb"),
                    new WordMatch("word", "wordis", 1, 0, "google"));
        }
    }
}