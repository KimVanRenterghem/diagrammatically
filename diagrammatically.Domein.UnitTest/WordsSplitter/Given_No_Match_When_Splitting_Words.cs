using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordsSplitter
{
    public class Given_No_Match_When_Splitting_Words : Given_When_Then
    {
        private Domein.WordsSplitter _sut;
        private IEnumerable<string> _res;

        protected override void Given()
        {
            _sut = new Domein.WordsSplitter(new[] { ' ', '_' }, new[] { 'A', 'N' });
        }

        protected override void When()
        {
            _res = _sut.Split("eenStoel");
        }

        [Fact]
        public void Then_The_Input_Is_Not_Splitted()
        {
            _res.Should().BeEquivalentTo("eenStoel");
        }
    }
}