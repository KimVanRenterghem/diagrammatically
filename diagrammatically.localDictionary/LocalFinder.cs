using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Pipe;
using diagrammatically.Domein;
using diagrammatically.Domein.Interfaces;

namespace diagrammatically.localDictionary
{
    public class LocalFinder : WordFonder
    {
        private readonly IMatchCalculator _matchCalculator;
        private Reposetry _repo;

        public LocalFinder(IMatchCalculator matchCalculator, Reposetry repo)
        {
            _matchCalculator = matchCalculator;
            _repo = repo;
        }

        public Task<IEnumerable<WordMatch>> ConsumeAsync(string input, IEnumerable<string> langs)
            => _repo.Get(input, langs)
                .Select(word
                    => new WordMatch(input, word.Id.Word, _matchCalculator.Calculate(input, word.Id.Word), word.Used, "localDb")
                )
                .ToList()
                .Pipe(Task.FromResult<IEnumerable<WordMatch>>);
    }
}