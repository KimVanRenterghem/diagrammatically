using System.Collections.Generic;
using System.Threading.Tasks;
using diagrammatically.Domein.Interfaces;
using diagrammatically.Domein.WordMatchConsumer;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.InputProseser
{
    public class When_having_input : Given_When_Then
    {
        private InputProsesers.InputProseser _sut;
        private readonly List<IEnumerable<WordMatch>> _matshes = new List<IEnumerable<WordMatch>>();

        protected override void Given()
        {
            var inputconsumerMock1 = new Mock<WordFonder>();
            inputconsumerMock1
                .Setup(inputconsumer => inputconsumer.ConsumeAsync("test", new[] { "nl" }))
                .Returns(Task.FromResult<IEnumerable<WordMatch>>(new[] { new WordMatch("test", "testkim", 10, 0, "") }));

            var inputconsumerMock2 = new Mock<WordFonder>();
            inputconsumerMock2
                .Setup(inputconsumer => inputconsumer.ConsumeAsync("test", new[] { "nl" }))
                .Returns(Task.FromResult<IEnumerable<WordMatch>>(new[] { new WordMatch("test", "testkim2", 10, 0, "") }));

            var optionconsumerMock = new Mock<Subscriber<IEnumerable<WordMatch>>>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Lisen(It.IsAny<IEnumerable<WordMatch>>(), "unittest", new[] { "nl" }))
                .Callback<IEnumerable<WordMatch>, string, IEnumerable<string>>((matches, source, langs) => _matshes.Add(matches));

            _sut = new InputProsesers.InputProseser
                (
                    new[] { inputconsumerMock1.Object, inputconsumerMock2.Object }
                );

            _sut.Subscribe(optionconsumerMock.Object);
        }

        protected override void When()
        {
            _sut.Lisen("test", "unittest", new[] { "nl" });
        }

        [Fact]
        public void Then_The_OprionsConsumer_Is_Called_A_List_Of_Posibiletys()
        {
            _matshes.Should().BeEquivalentTo(
                new[] { new WordMatch("test", "testkim", 10, 0, "") },
                new[] { new WordMatch("test", "testkim2", 10, 0, "") }
                );
        }
    }
}
