using System.Collections.Generic;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein
{
    public class WordSelector : Subscriber<WordSelection>, Publisher<WordSelection>
    {
        private readonly OutputWriter _writer;
        private readonly InputStreamGenerator _inputStreamGenerator;
        private readonly List<Subscriber<WordSelection>> _subscribers = new List<Subscriber<WordSelection>>();

        public WordSelector(OutputWriter writer, InputStreamGenerator inputStreamGenerator)
        {
            _writer = writer;
            _inputStreamGenerator = inputStreamGenerator;
        }

        public void Lisen(WordSelection selectedWord, string source, IEnumerable<string> langs)
        {
            _inputStreamGenerator.StopInput();
            _writer.Write(selectedWord.Word,selectedWord.Typed,selectedWord.Sourse);
            _inputStreamGenerator.StartInput();
            _subscribers
                .ForEach(subscriber => subscriber.Lisen(selectedWord, source, langs));
        }

        public void Subscribe(Subscriber<WordSelection> subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}