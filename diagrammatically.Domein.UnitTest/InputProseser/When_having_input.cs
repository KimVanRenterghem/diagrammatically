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
            var inputconsumerMock1 = new Mock<IInputConsumer>();
            inputconsumerMock1
                .Setup(inputconsumer => inputconsumer.ConsumeAsync("test", new[] { "nl" }))
                .Returns(Task.FromResult<IEnumerable<WordMatch>>(new[] { new WordMatch("test", "testkim", 10, 0, "") }));

            var inputconsumerMock2 = new Mock<IInputConsumer>();
            inputconsumerMock2
                .Setup(inputconsumer => inputconsumer.ConsumeAsync("test", new[] { "nl" }))
                .Returns(Task.FromResult<IEnumerable<WordMatch>>(new[] { new WordMatch("test", "testkim2", 10, 0, "") }));

            var optionconsumerMock = new Mock<IWordMatchConsumerConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Consume("test", "unittest", It.IsAny<IEnumerable<WordMatch>>()))
                .Callback<string, string, IEnumerable<WordMatch>>((filter,source,matches) => _matshes.Add(matches));

            _sut = new InputProsesers.InputProseser
                (
                    new[] { inputconsumerMock1.Object, inputconsumerMock2.Object },
                    new[] { optionconsumerMock.Object }
                );
        }

        protected override void When()
        {
            _sut.Loockup("test", "unittest", new[] { "nl" });
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
