using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using diagrammatically.AvaloniaUi;
using diagrammatically.Domein;
using Keystroke.API;

namespace diagrammatically.electron_edge.api
{
    public class OptionApi
    {
        private readonly IDependencysBuilder _dependencysBuilder;
        private readonly KeystrokeAPI _keystrokeApi;
        public static IEnumerable<WordMatch> _LastMatches = new List<WordMatch>();
        private CancellationToken _cancel;

        public OptionApi() : this(new DependencysBuilder(), new KeystrokeAPI())
        {
            
        }

        public OptionApi(IDependencysBuilder dependencysBuilder, KeystrokeAPI keystrokeApi)
        {
            _dependencysBuilder = dependencysBuilder;
            _keystrokeApi = keystrokeApi;
        }

        public async Task<object> GetCurrentMatches(dynamic input)
            => _LastMatches;

        public async Task<object> StartLogger(dynamic input)
        {
            _cancel = new CancellationTokenSource().Token;
            Task.Run(() => Startapplication());

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

            _keystrokeApi.CreateKeyboardHook(key 
                => _dependencysBuilder
                    .InputGenerator
                    .Genrrate(new[] { "nl", "en" })(key)
                );

            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>();
        }
    }
}