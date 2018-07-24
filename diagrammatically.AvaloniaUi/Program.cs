using System.Threading.Tasks;
using Avalonia;
using diagrammatically.Domein;
using diagrammatically.Domein.InputProsesers;
using diagrammatically.Domein.Interfaces;
using diagrammatically.Domein.WordMatchConsumer;
using diagrammatically.localDictionary;
using Keystroke.API;

namespace diagrammatically.AvaloniaUi
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var api = new KeystrokeAPI())
            {
                InputGenerator inputGenerator = null;
                
                Task.Run(() =>
                {
                    while (inputGenerator == null)
                    {
                        if (MainWindow.Singel != null)
                        {
                            var inputprosecer = BuildDependencys(MainWindow.Singel);
                            inputGenerator = new InputGenerator(inputprosecer);
                        }
                        else
                        {
                            Task.Delay(30).Wait();
                        }
                    }
                });

                api.CreateKeyboardHook(key => inputGenerator.Genrrate(new[] { "nl", "en"})(key));

                BuildAvaloniaApp()
                    .Start<MainWindow>();

            }
        }

        private static IInputProseser BuildDependencys(MainWindow main)
        {
            var wordSpliters = " _\n\t".ToCharArray();
            var sentensSpliters = "<>\\/+=-*.,!?:;()&|".ToCharArray();
            var uppers = "ABCDEFGHIJKLMNOPQRSTUVWYYZ".ToCharArray();

            var wordsplitter = new WordsSplitter(wordSpliters, uppers);
            var sentenssplitter = new WordsSplitter(sentensSpliters, new char[0]);


            var matchCalculator = new MatchCalculator();

            var reposetry = new Reposetry(matchCalculator);

            var localFinder = new LocalFinder(matchCalculator, reposetry);

            var inputConsumers = new IInputConsumer[]
            {
               // new SearchConsumer(),
                localFinder
            };

            var optionconsumer = new WordMatchesConsumer(main.SetWords);
            var proitizer = new WordMatchPriotizeConsumer(new []{ optionconsumer}, 8);
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


            return splitter;
        }

        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect();
    }
}

