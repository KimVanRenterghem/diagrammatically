namespace diagrammatically.Domein
{
    public class WordSelection
    {
        public string Word { get; }
        public string Typed { get; }
        public string Sourse { get; }

        public WordSelection(string word, string typed, string sourse)
        {
            Word = word;
            Typed = typed;
            Sourse = sourse;
        }
    }
}