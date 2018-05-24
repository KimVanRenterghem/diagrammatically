using Avalonia;
using Avalonia.Markup.Xaml;

namespace diagrammatically.AvaloniaUi
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}