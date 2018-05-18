using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace diagrammatically.localDictionary
{
    public class Reposetry
    {
        private string connextion = "Filename=words.db;Flush=True";
        public void Add(Word word)
        {
            using (var db = new LiteDatabase(connextion))
            {
                var col = db.GetCollection<Word>("words");
                word.Id.Word = word.Id.Word.ToLower();

                if (col.Find(x => x.Id == word.Id && x.Id.Lang == word.Id.Lang).Any()) 
                    return;
                
                col.Insert(word);
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
                       word.Length <= x.Id.Word.Length &&
                      ((double)word
                           .Where((letter, index) => x.Id.Word[index] == letter)
                           .Count() / (double)x.Id.Word.Length >= 0.6)));
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