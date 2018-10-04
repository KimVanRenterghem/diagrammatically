using System.Collections.Generic;
using diagrammatically.AvaloniaUi;
using diagrammatically.Domein;
using diagrammatically.Domein.InputProsesers;
using diagrammatically.Domein.Interfaces;
using diagrammatically.Domein.WordMatchConsumer;
using diagrammatically.localDictionary;

namespace diagrammatically.electron_edge.api
{
    public class DependencysBuilder : IDependencysBuilder
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

            InputGenerator = new InputGenerator();
            var splitter = new CreateWordInPut(wordsplitter, sentenssplitter);
           
            var proitizer = new WordMatchPriotizeConsumer(8);
            var optionZiper = new WordMatchZipConsumer();

            var optionconsumerAPI = new WordMatchesConsumer();
            var optionconsumerAvalonia = new AvaloniaUi.WordMatchesConsumer(main.SetWords);
            
            var localFinder = new LocalFinder(matchCalculator, reposetry);
            var inputConsumers = new WordFonder[]
            {
                // new SearchConsumer(),
                localFinder
            };

            var inputProseser = new InputProseser(inputConsumers);

            InputGenerator.Subscribe(splitter);
            splitter.Subscribe(inputProseser);

            inputProseser.Subscribe(optionZiper);
            optionZiper.Subscribe(proitizer);
            
            proitizer.Subscribe(optionconsumerAPI);
            proitizer.Subscribe(optionconsumerAvalonia);

            main.MatchCalculator = matchCalculator;
            main.ViewModel = new ViewModel();
            main.Reposetry = reposetry;
        }
    }
}