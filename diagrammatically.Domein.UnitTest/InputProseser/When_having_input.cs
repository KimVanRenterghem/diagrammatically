﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.InputProseser
{
    public class When_having_input : Given_When_Then
    {
        private Domein.InputProseser _sut;        
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

            var optionconsumerMock = new Mock<IOptionConsumer>();
            optionconsumerMock
                .Setup(optionconsumer => optionconsumer.Consume(It.IsAny<IEnumerable<WordMatch>>()))
                .Callback<IEnumerable<WordMatch>>(matches => _matshes.Add(matches));

            _sut = new Domein.InputProseser
                (
                    new[] { inputconsumerMock1.Object, inputconsumerMock2.Object },
                    new[] { optionconsumerMock.Object }
                );
        }

        protected override void When()
        {
            _sut.Loockup("test", new[] { "nl" });
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