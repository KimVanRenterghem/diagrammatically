using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.OptionZipConsumer
{
    public class When_Consuming : Given_When_Then
    {
        private Domein.OptionZipConsumer _sub;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var optionconsumerMock = new Mock<IOptionConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Consume("word", "vs code", It.IsAny<IEnumerable<WordMatch>>()))
                .Callback<string, string, IEnumerable<WordMatch>>((filter, source, matches) => _matshes.Add(matches));

            _sub = new Domein.OptionZipConsumer(optionconsumerMock.Object);
        }

        protected override void When()
        {
            var filter = "woord";

            _sub.Consume("word", "vs code", new[]
            {
                new WordMatch("word", "worden", 1, 0, "localdb"),
                new WordMatch("word", "word", 0.1, 0, "localdb")
            });
            _sub.Consume("word", "vs code", new[]
            {
                new WordMatch("word", "wordis", 1, 0, "google") ,
                new WordMatch("word", "worden", 0.3, 0, "google")
            });
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