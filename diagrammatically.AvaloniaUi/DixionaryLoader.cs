using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein;
using diagrammatically.localDictionary;

namespace diagrammatically.AvaloniaUi
{
    public class DixionaryLoader
    {
        private readonly Reposetry _repo;
        private readonly IMatchCalculator _calculator;

        public DixionaryLoader(Reposetry repo, IMatchCalculator calculator)
        {
            _repo = repo;
            _calculator = calculator;
        }

        /// <summary>
        /// uses https://github.com/KimVanRenterghem/Dictionaries .dic file as seed
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lang"></param>
        public void Load(string path, string lang)
        {
            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine()
                        ?.Split('/')
                        .First()
                        .ToLower();

                    if (!string.IsNullOrEmpty(word))
                    {
                        using (var file = new StreamWriter($"words_{_calculator.Replace(word.First())}_{lang}.txt",
                            true))
                        {
                            file.WriteLine(word);
                        }
                    }
                }
            }

            Directory.CreateDirectory("Data/Words/");

            Directory.GetFiles(Directory.GetCurrentDirectory(), "words_*.txt")
                .Select(RealLetterWords)
                .ToList()
                .Pipe(Task.WhenAll)
                .Wait();

            Directory.GetFiles(Directory.GetCurrentDirectory(), "words_*.txt")
                .ForEach(File.Delete);
        }

        private async Task RealLetterWords(string filepath)
        {
            await Task.Run(() => ReadWordsFile());

            void ReadWordsFile()
            {
                var lang = filepath
                    .Split('\\')
                    .Last()
                    .Split('.')
                    .First()
                    .Split('_')
                    .Last();

                using (var reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var word = reader.ReadLine();
                        var wordObject = new Word
                        {
                            Id = new WordKey
                            {
                                Word = word,
                                Lang = lang
                            }
                        };
                        _repo.Add(wordObject);
                    }
                }
            }
        }
    }
}