using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.Domein.InputProsesers
{
    public class CreateWordInPut : IInputProseser
    {
        private readonly IInputProseser _inputProseser;
        private readonly IWordsSplitter _wordsSplitter;
        private readonly IWordsSplitter _sentensSplitter;

        public CreateWordInPut(IInputProseser inputProseser, IWordsSplitter wordsSplitter, IWordsSplitter sentensSplitter)
        {
            _inputProseser = inputProseser;
            _wordsSplitter = wordsSplitter;
            _sentensSplitter = sentensSplitter;
        }

        public void Loockup(string filter, string source, IEnumerable<string> langs)
        {
            var sentenses = _sentensSplitter.Split(filter);
            filter = sentenses.Last();

            var words = _wordsSplitter.Split(filter);

            filter = words.Last();
            _inputProseser.Loockup(filter, source, langs);
        }
    }
}