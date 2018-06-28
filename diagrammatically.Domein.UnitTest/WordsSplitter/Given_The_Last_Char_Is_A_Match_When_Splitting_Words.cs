using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordsSplitter
{
    public class Given_The_Last_Char_Is_A_Match_When_Splitting_Words : Given_When_Then
    {
        private Domein.WordsSplitter _sut;
        private IEnumerable<string> _res;

        protected override void Given()
        {
            _sut = new Domein.WordsSplitter(new[] { ' ', '.' }, new[] { 'A', 'S' });
        }

        protected override void When()
        {
            _res = _sut.Split("een Stoel.");
        }

        [Fact]
        public void Then_The_Input_Is_Splitted()
        {
            _res.Should().BeEquivalentTo("een", "Stoel");
        }
    }
}