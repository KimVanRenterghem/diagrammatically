using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace diagrammatically.Domein.Test
{
    [TestClass]
    public class When_having_input : Given_When_Then
    {
        private InputProseser _sut;
        private IEnumerable<IEnumerable<string>> _res;
        private List<IEnumerable<string>> _options = new List<IEnumerable<string>>();

        protected override void Given()
        {
            var inputconsumerMock1 = new Mock<IInputConsumer>();
            inputconsumerMock1
                .Setup(inputconsumer => inputconsumer.Consume("test"))
                .Returns(Task.FromResult<IEnumerable<string>>(new[] { "testkim" }));

            var inputconsumerMock2 = new Mock<IInputConsumer>();
            inputconsumerMock2
                .Setup(inputconsumer => inputconsumer.Consume("test"))
                .Returns(Task.FromResult<IEnumerable<string>>(new[] { "testkim2" }));

            var optionconsumerMock = new Mock<IOptionConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Consume(It.IsAny<IEnumerable<string>>()))
                .Callback<IEnumerable<string>>(options => _options.Add(options));

            _sut = new InputProseser
                (
                    new[] { inputconsumerMock1.Object, inputconsumerMock2.Object },
                    new[] { optionconsumerMock.Object }
                );
        }

        protected override void When()
        {
            _res = _sut.Loockup("test");
        }

        [TestMethod]
        public void Then_There_Will_Be_A_List_Of_Posibiletys()
        {
            _res.Should().BeEquivalentTo(new[] { "testkim" }, new[] { "testkim2" });
        }

        [TestMethod]
        public void Then_The_OprionsConsumer_Is_Called_A_List_Of_Posibiletys()
        {
            _options.Should().BeEquivalentTo(new[] { "testkim" }, new[] { "testkim2" });
        }
    }
}
