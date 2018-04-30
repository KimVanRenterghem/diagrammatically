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

        protected override void Given()
        {
            var inputconsumerMock1 = new Mock<InputConsumer>();
            inputconsumerMock1.Setup(inputconsumer => inputconsumer.Consume("test"))
                .Returns(Task.FromResult<IEnumerable<string>>(new[] { "testkim" }));
            var inputconsumerMock2 = new Mock<InputConsumer>();
            inputconsumerMock2.Setup(inputconsumer => inputconsumer.Consume("test"))
                .Returns(Task.FromResult<IEnumerable<string>>(new[] { "testkim2" }));

            _sut = new InputProseser(new []{ inputconsumerMock1.Object, inputconsumerMock2.Object });
        }

        protected override void When()
        {
            _res = _sut.Loockup("test");
        }

        [TestMethod]
        public void Then_There_Will_Be_A_List_Of_Posibiletys()
        {
            _res.Should().BeEquivalentTo(new[] {"testkim"}, new[] { "testkim2" });
        }
    }
}
