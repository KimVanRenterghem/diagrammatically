using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.CreateWordInPut
{
    public class When_Input_Is_With_Kammelcase : Given_When_Then
    {
        private Domein.CreateWordInPut _sub;
        private string _search;

        protected override void Given()
        {
            var inputProseserMock = new Mock<IInputProseser>();

            inputProseserMock
                .Setup(inputProseser => inputProseser.Loockup(It.IsAny<string>(), new[] {"en"}))
                .Callback<string,IEnumerable<string>>((search,lang) => _search = search);


            _sub = new Domein.CreateWordInPut(inputProseserMock.Object);
        }

        protected override void When()
        {
            _sub.Loockup("GivenWhenThen", new []{"en"});
        }

        [Fact]
        public void Then_The_Last_Word_Shoold_Be_Searched()
        {
            _search.Should().Be("Then");
        }
    }
}