using System.Collections.Generic;
using diagrammatically.Domein.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace diagrammatically.Domein.UnitTest.WordSelector
{
    public class When_Selecting : Given_When_Then
    {
        private Domein.WordSelector _sub;
        private readonly Stack<string> _flow = new Stack<string>();
        private string _resuld;
        private WordSelection _selection;

        protected override void Given()
        {
            var outputWriter = new Mock<OutputWriter>();
            outputWriter
                .Setup(writer => writer.Write(It.IsAny<string>(),"stm","test"))
                .Callback<string,string,string>((word, typedWord, sourse) =>
                {
                    _resuld = word;
                    _flow.Push("Write Word");
                });


            var WordSelectionSubscriber = new Mock<Subscriber<WordSelection>>();
            WordSelectionSubscriber
                .Setup(lisener => lisener.Lisen( It.IsAny<WordSelection>(), "test", new[] { "ts" }))
                .Callback<WordSelection, string, IEnumerable<string>>((selection, source, langs) => _selection = selection);

            var inputStreamGenerator = new Mock<InputStreamGenerator>();
            inputStreamGenerator
                .Setup(input => input.StartInput())
                .Callback(() => _flow.Push("StartInput"));

            inputStreamGenerator
                .Setup(input => input.StopInput())
                .Callback(() => _flow.Push("StopInput"));


            _sub = new Domein.WordSelector(outputWriter.Object, inputStreamGenerator.Object);
            _sub.Subscribe(WordSelectionSubscriber.Object);

        }

        protected override void When()
        {
            var selection = new WordSelection("stoel", "stm", "test");
            _sub.Lisen(selection, "test", new[] { "ts" });
        }

        [Fact]
        public void Then_The_WordShooldBeTyped()
        {
            _resuld.Should().Be("stoel");
        }

        [Fact]
        public void Then_The_Subscribers_Resive()
        {
            _selection.Word.Should().Be("stoel");
            _selection.Sourse.Should().Be("test");
            _selection.Typed.Should().Be("stm");
        }

        [Fact]
        public void Then_The_Is_Stoped_And_Restarted()
        {
            _flow.Pop().Should().Be("StartInput");
            _flow.Pop().Should().Be("Write Word");
            _flow.Pop().Should().Be("StopInput");
            _flow.Count.Should().Be(0);
        }
    }
}