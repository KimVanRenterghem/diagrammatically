﻿using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordSplitter
{
    public class When_Input_Is_With_Underscore : Given_When_Then
    {
        private Domein.WordSplitter _sub;
        private string _search;

        protected override void Given()
        {
            var inputProseserMock = new Mock<IInputProseser>();

            inputProseserMock
                .Setup(inputProseser => inputProseser.Loockup(It.IsAny<string>(), new[] {"nl"}))
                .Callback<string,IEnumerable<string>>((search,lang) => _search = search);


            _sub = new Domein.WordSplitter(inputProseserMock.Object);
        }

        protected override void When()
        {
            _sub.Loockup("bet_en", new []{"nl"});
        }

        [Fact]
        public void Then_The_Last_Word_Shoold_Be_Searched()
        {
            _search.Should().Be("en");
        }
    }
}