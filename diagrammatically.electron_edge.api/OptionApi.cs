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
        public static IEnumerable<WordMatch> _LastMatches = new List<WordMatch>();
        private CancellationToken _cancel;

        public async Task<object> GetCurrentMatches(dynamic input)
            => _LastMatches;

        public async Task<object> StartLogger(dynamic input)
        {
            _cancel = new CancellationTokenSource().Token;
            Task.Run(() => Startapplication());

            return Task.FromResult<object>(true);
        }

        private static void Startapplication()
        {
            var api = new KeystrokeAPI();
            
            var builder = new DependencysBuilder();

            Task.Run(() =>
            {
                while (builder.InputGenerator == null)
                {
                    if (MainWindow.Singel != null)
                    {
                        builder.Build(MainWindow.Singel);
                    }
                    else
                    {
                        Task.Delay(30).Wait();
                    }
                }
            });

            api.CreateKeyboardHook(key 
                => builder
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