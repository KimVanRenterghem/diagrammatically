using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace diagrammatically.Domein.UnitTest.AllIndexesOf
{
    public class GivenNoMatchWhenFindingIndexes : Given_When_Then
    {
        private string _sub;
        private IEnumerable<int> _res;

        protected override void Given()
        {
            _sub = "testword";
        }

        protected override void When()
        {
            _res = _sub.AllIndexesOf('Z');
        }

        [Fact]
        public void Then_Two_Resulds_Are_Found()
        {
            _res.Should().BeEquivalentTo(new int[0]);
        }
    }
}