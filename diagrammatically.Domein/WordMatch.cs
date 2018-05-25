namespace diagrammatically.Domein
{
    public class WordMatch
    {
        public string Search { get; }
        public string Word { get; }
        public double Match { get; }
        public int Used { get; }
        public string Sourse { get; }

        public WordMatch(string search, string word, double match, int used, string sourse)
        {
            Search = search;
            Word = word;
            Match = match;
            Used = used;
            Sourse = sourse;
        }
    }
}