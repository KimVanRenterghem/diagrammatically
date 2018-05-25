using System.Collections.Generic;
using System.Linq;
using diagrammatically.Domein;
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

        private string connextion = "Filename=words.db;Flush=True";
        public void Add(Word word)
        {
            if (string.IsNullOrEmpty(word.Id.Word) || string.IsNullOrEmpty(word.Id.Lang))
                return;

            using (var db = new LiteDatabase(connextion))
            {
                var col = db.GetCollection<Word>("words");
                word.Id.Word = word.Id.Word.ToLower();

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
            using (var db = new LiteDatabase(connextion))
            {
                word = word.ToLower();
                var col = db.GetCollection<Word>("words");

                return col.Find(x
                    => langs.Any(l => x.Id.Lang == l) && (x.Id.Word.StartsWith(word) ||
                       word.Length <= x.Id.Word.Length && _matchCalculator.Calculate(word, x.Id.Word) >= 0.6));
            }
        }

        public void Drop()
        {
            using (var db = new LiteDatabase(connextion))
            {
                db.DropCollection("words");
            }
        }
    }
}