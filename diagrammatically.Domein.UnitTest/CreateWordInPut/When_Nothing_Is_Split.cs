﻿using System.Collections.Generic;
using diagrammatically.Domein.InputProsesers;
using diagrammatically.Domein.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.CreateWordInPut
{
    public class When_Nothing_Is_Split : Given_When_Then
    {
        private InputProsesers.CreateWordInPut _sub;
        private string _search;

        protected override void Given()
        {
            var inputProseserMock = new Mock<InputProseserStream>();

            inputProseserMock
                .Setup(inputProseser => inputProseser.Lisen(It.IsAny<string>(), It.IsAny<string>(), new[] {"en"}))
                .Callback<string, string, IEnumerable<string>>((search, source,lang) => _search = search);

            var sentenssplitter = new Mock<IWordsSplitter>();
            sentenssplitter
                .Setup(splitter => splitter.Split("Given WhenThen"))
                .Returns(() => new[] { "Given WhenThen" });

            var wordsplitter = new Mock<IWordsSplitter>();
            wordsplitter
                .Setup(splitter => splitter.Split("Given WhenThen"))
                .Returns(() => new[] { "Given WhenThen" });

            _sub = new InputProsesers.CreateWordInPut(wordsplitter.Object, sentenssplitter.Object);

            _sub.Subscribe(inputProseserMock.Object);
        }

        protected override void When()
        {
            _sub.Lisen("Given WhenThen", "untitest", new []{"en"});
        }

        [Fact]
        public void Then_The_Last_Word_Shoold_Be_Searched()
        {
            _search.Should().Be("Given WhenThen");
        }
    }
}