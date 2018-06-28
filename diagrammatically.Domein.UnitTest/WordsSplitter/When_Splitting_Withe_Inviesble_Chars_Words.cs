using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordsSplitter
{
    public class When_Splitting_Withe_Inviesble_Chars_Words : Given_When_Then
    {
        private Domein.WordsSplitter _sut;
        private IEnumerable<string> _res;

        protected override void Given()
        {
            _sut = new Domein.WordsSplitter(new[] { ' ', '_' }, new[] { 'A', 'B' });
        }

        protected override void When()
        {
            _res = _sut.Split("een stoel");
        }

        [Fact]
        public void Then_The_Input_Is_Splitted()
        {
            _res.Should().BeEquivalentTo("een", "stoel");
        }
    }
}