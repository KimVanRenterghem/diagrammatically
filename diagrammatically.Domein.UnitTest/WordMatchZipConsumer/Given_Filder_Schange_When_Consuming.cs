using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.WordMatchConsumer;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordMatchZipConsumer
{
    public class Given_Filder_Schange_When_Consuming : Given_When_Then
    {
        private WordMatchConsumer.WordMatchZipConsumer _sub;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var optionconsumerMock = new Mock<IWordMatchConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Lisen(It.IsAny<IEnumerable<WordMatch>>(), "vs code", It.IsAny<IEnumerable<string>>()))
                .Callback<IEnumerable<WordMatch>, string, IEnumerable<string>>((matches, source, langs) => _matshes.Add(matches));

            _sub = new WordMatchConsumer.WordMatchZipConsumer();
            _sub.Subscribe(optionconsumerMock.Object);
        }

        protected override void When()
        {
            var filter = "woord";

            _sub.Lisen(
                new[]
                {
                    new WordMatch("word", "worden", 1, 0, "localdb"),
                    new WordMatch("word", "word", 0.1, 0, "localdb")
                },
                "vs code",
                new[] { "ln" });

            _sub.Lisen(
                new[]
                {
                    new WordMatch("worde", "wordis", 1, 0, "google") ,
                    new WordMatch("worde", "worden", 0.3, 0, "google")
                },
                "vs code",
                new[] { "ln" });
        }

        [Fact]
        public void Then_The_Lists_Are_Not_Joined()
        {
            _matshes
                .Last()
                .Should().BeEquivalentTo(
                    new WordMatch("worde", "wordis", 1, 0, "google"),
                    new WordMatch("worde", "worden", 0.3, 0, "google"));
        }
    }
}