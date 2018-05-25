using Avalonia;
using Avalonia.Markup.Xaml;

namespace diagrammatically.AvaloniaUi
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
   }
}