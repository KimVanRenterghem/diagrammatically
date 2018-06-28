using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using Keystroke.API;

namespace diagrammatically.AvaloniaUi
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) => { Console.Write(character); });



                //System.Windows.Forms.Application.EnableVisualStyles();
                //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                //AppBuilder.Configure<App>().UseWin32().UseDirect2D1().SetupWithoutStarting();
                //System.Windows.Forms.Application.Run(new MainWindow());
                BuildAvaloniaApp()
                    .Start<MainWindow>();
            }
        }

        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect();
    }
}

