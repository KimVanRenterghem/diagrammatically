namespace diagrammatically.localDictionary
{
    public class Word
    {
        public WordKey Id { get; set; }
        public int Used { get; set; }
    }

    public class WordKey
    {
        public string Word { get; set; }
        public string Lang { get; set; }
    }
}