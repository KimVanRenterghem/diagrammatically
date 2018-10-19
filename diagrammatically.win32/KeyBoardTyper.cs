using System.Linq;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.win32
{
    public class KeyBoardTyper : OutputWriter
    {
        public void Write(string word, string typedWord, string sourse)
        {
            Enumerable
                .Repeat(new Key(Messaging.VKeys.KEY_BACK), typedWord.Length)
                .ToList()
                .ForEach(k => k.PressForeground());

            word
                .Select(w => new Key(w))
                .ToList()
                .ForEach(k => k.PressForeground());
        }
    }
}
