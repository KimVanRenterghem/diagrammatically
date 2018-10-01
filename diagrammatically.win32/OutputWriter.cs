using System.Linq;

namespace diagrammatically.win32
{

    public class OutputWriter
    {
        public void Write(string word, string typedWord)
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
