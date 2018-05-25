using System.IO;
using System.Linq;
using diagrammatically.localDictionary;

namespace diagrammatically.AvaloniaUi
{
    public class DixionaryLoader
    {
        private readonly Reposetry _repo;

        public DixionaryLoader(Reposetry repo)
        {
            _repo = repo;
        }
        public void Load(string path, string lang)
        {
            //uses https://github.com/KimVanRenterghem/Dictionaries .dic file as seed
            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine()?.Split('/').First();
                    _repo.Add(new Word
                    {
                        Id = new WordKey
                        {
                            Word = word,
                            Lang = lang
                        }
                    });
                }
            }
        }
    }
}