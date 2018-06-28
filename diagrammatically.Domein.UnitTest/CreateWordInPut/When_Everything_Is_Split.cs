using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.CreateWordInPut
{
    public class When_Everything_Is_Split : Given_When_Then
    {
        private Domein.CreateWordInPut _sub;
        private string _search;

        protected override void Given()
        {
            var inputProseserMock = new Mock<IInputProseser>();

            inputProseserMock
                .Setup(inputProseser => inputProseser.Loockup(It.IsAny<string>(), new[] {"en"}))
                .Callback<string,IEnumerable<string>>((search,lang) => _search = search);

            var sentenssplitter = new Mock<IWordsSplitter>();
            sentenssplitter
                .Setup(splitter => splitter.Split("Given WhenThen"))
                .Returns(() => new[] { "Given", "WhenThen" });

            var wordsplitter = new Mock<IWordsSplitter>();
            wordsplitter
                .Setup(splitter => splitter.Split("WhenThen"))
                .Returns(() => new[] { "When", "Then" });

            _sub = new Domein.CreateWordInPut(inputProseserMock.Object, wordsplitter.Object, sentenssplitter.Object);
        }

        protected override void When()
        {
            _sub.Loockup("Given WhenThen", new []{"en"});
        }

        [Fact]
        public void Then_The_Last_Word_Shoold_Be_Searched()
        {
            _search.Should().Be("Then");
        }
    }
}