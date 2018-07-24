using diagrammatically.AvaloniaUi;
using diagrammatically.Domein;
using diagrammatically.Domein.InputProsesers;
using diagrammatically.Domein.Interfaces;
using diagrammatically.Domein.WordMatchConsumer;
using diagrammatically.localDictionary;

namespace diagrammatically.electron_edge.api
{
    public class DependencysBuilder
    {
        public InputGenerator InputGenerator { get; private set; }

        public void Build(MainWindow main)
        {
            var wordSpliters = " _\n\t".ToCharArray();
            var sentensSpliters = "<>\\/+=-*.,!?:;()&|".ToCharArray();
            var uppers = "ABCDEFGHIJKLMNOPQRSTUVWYYZ".ToCharArray();

            var wordsplitter = new WordsSplitter(wordSpliters, uppers);
            var sentenssplitter = new WordsSplitter(sentensSpliters, new char[0]);


            var matchCalculator = new MatchCalculator();

            var reposetry = new Reposetry(matchCalculator, @"C:\git\diagrammatically\");

            var localFinder = new LocalFinder(matchCalculator, reposetry);

            var inputConsumers = new IInputConsumer[]
            {
                // new SearchConsumer(),
                localFinder
            };

            var optionconsumer = new WordMatchesConsumer();

            var optionconsumer2 = new AvaloniaUi.WordMatchesConsumer(main.SetWords);

            var consumers = new IWordMatchConsumer[] { optionconsumer, optionconsumer2 };
            var proitizer = new WordMatchPriotizeConsumer(consumers, 8);
            var optionZiper = new WordMatchZipConsumer(proitizer);


            var optionConsumers = new[]
            {
                optionZiper
            };

            var inputProseser = new InputProseser(inputConsumers, optionConsumers);
            var splitter = new CreateWordInPut(inputProseser, wordsplitter, sentenssplitter);


            main.MatchCalculator = matchCalculator;

            main.ViewModel = new ViewModel();
            main.Reposetry = reposetry;

            InputGenerator = new InputGenerator(splitter);
        }
    }
}