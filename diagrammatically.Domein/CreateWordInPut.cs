using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
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

        public void Loockup(string filter, IEnumerable<string> langs)
        {
            //test for words and sentenses
            //add ISentensConsumer for secendlast
            var sentenses = _sentensSplitter.Split(filter);
            filter = sentenses.Last();

            var words = _wordsSplitter.Split(filter);

            filter = words.Last();
            _inputProseser.Loockup(filter, langs);
        }
    }
}