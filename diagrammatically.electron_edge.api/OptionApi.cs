using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using diagrammatically.AvaloniaUi;
using diagrammatically.Domein;
using diagrammatically.win32;
using Keystroke.API;

namespace diagrammatically.electron_edge.api
{
    public class OptionApi
    {
        private static IDependencysBuilder _dependencysBuilder;
        private static readonly IEnumerable<string> _langs = new[] { "nl", "en" };
        private readonly KeystrokeAPI _keystrokeApi;
        public static IEnumerable<WordMatch> _LastMatches = new List<WordMatch>();
        private CancellationToken _cancel;

        public OptionApi() : this(new DependencysBuilder(), new KeystrokeAPI())
        {

        }

        public OptionApi(IDependencysBuilder dependencysBuilder, KeystrokeAPI keystrokeApi)
        {
            if (_dependencysBuilder == null)
                _dependencysBuilder = dependencysBuilder;
            _keystrokeApi = keystrokeApi;
        }

        public async Task<object> GetCurrentMatches(dynamic input)
            => _LastMatches;

        public async Task<object> StartLogger(dynamic input)
        {
            _cancel = new CancellationTokenSource().Token;
            Task.Run(() =>
            {
                Startapplication();
            });

            return Task.FromResult<object>(true);
        }

        public async Task<object> SelectWord(dynamic input)
        {
            var selectedWord = new WordSelection(
                input.selection.ToString(),
                input.typed.ToString(),
                input.sourse.ToString());

            if (string.IsNullOrEmpty(selectedWord.Word))
                return Task.FromResult<object>(true);

            Task.Run(() =>
            {
                _dependencysBuilder
                    .WordSelector
                    .Lisen(selectedWord, selectedWord.Sourse, _langs);
            });

            return Task.FromResult<object>(true);
        }

        private void Startapplication()
        {
            Task.Run(() =>
            {
                var delai = Task.Delay(30);
                while (_dependencysBuilder.InputGenerator == null)
                {
                    if (MainWindow.Singel != null)
                    {
                        _dependencysBuilder.Build(MainWindow.Singel);
                    }
                    else
                    {
                        delai.Wait(_cancel);
                    }
                }
            });

            _keystrokeApi.CreateKeyboardHook(key =>
                _dependencysBuilder
                    .InputGenerator
                    .GenerateKeyLisener(_langs)(key)
                );

            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>();
        }
    }
}