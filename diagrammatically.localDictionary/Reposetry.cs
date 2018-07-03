using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein;
using diagrammatically.Domein.Interfaces;
using LiteDB;

namespace diagrammatically.localDictionary
{
    public class Reposetry
    {
        private readonly IMatchCalculator _matchCalculator;

        public Reposetry(IMatchCalculator matchCalculator)
        {
            _matchCalculator = matchCalculator;
        }

        private string Connextion(WordKey word)
        {
            var first = word.Word
                .First()
                .Pipe(_matchCalculator.Replace);

            return $"Filename=Data/Words/words_{first}_ {word.Lang}.db;Flush=True";
        }

        public void Add(Word word)
        {
            if (string.IsNullOrEmpty(word.Id.Word) || string.IsNullOrEmpty(word.Id.Lang))
                return;

            word.Id.Word = word.Id.Word.ToLower();

            using (var db = new LiteDatabase(Connextion(word.Id)))
            {
                var col = db.GetCollection<Word>("words");

                try
                {
                    col.Insert(word);
                }
                catch (LiteException e) when (e.Message.Contains("Cannot insert duplicate key in unique index '_id'."))
                {
                    // ignore existing recorts
                }
            }
        }

        public IEnumerable<Word> Get(string word, IEnumerable<string> langs)
        {
            word = word.ToLower();

            return langs
                .Select(lang => new WordKey
                {
                    Lang = lang,
                    Word = word
                })
                .SelectMany(wordKey =>
                {
                    using (var db = new LiteDatabase(Connextion(wordKey)))
                    {
                        var col = db.GetCollection<Word>("words");

                        return col.Find(x
                            => x.Id.Word.StartsWith(word) ||
                               word.Length <= x.Id.Word.Length && _matchCalculator.Calculate(word, x.Id.Word) >= 0.6);
                    }
                })
                .ToList();
        }

        public void Drop()
        {
            Task.Delay(750).Wait();
            Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "words_*.db").ForEach(File.Delete);
        }
    }
}