using System.Collections.Generic;
using System.Linq;

namespace diagrammatically.Domein
{
    public class WordSplitter : IInputProseser
    {
        private IInputProseser _inputProseser;

        public WordSplitter(IInputProseser inputProseser)
        {
            _inputProseser = inputProseser;
        }

        public void Loockup(string filter, IEnumerable<string> langs)
        {
            filter = filter
                .Split(' ')
                .Last()
                .Split('_')
                .Last();
            _inputProseser.Loockup(filter, langs);
        }
    }
}