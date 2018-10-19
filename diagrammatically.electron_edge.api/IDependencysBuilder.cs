using diagrammatically.AvaloniaUi;
using diagrammatically.Domein;

namespace diagrammatically.electron_edge.api
{
    public interface IDependencysBuilder
    {
        InputGenerator InputGenerator { get; }
        WordSelector WordSelector { get; }
        void Build(MainWindow main);
    }
}