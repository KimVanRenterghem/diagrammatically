using diagrammatically.AvaloniaUi;

namespace diagrammatically.electron_edge.api
{
    public interface IDependencysBuilder
    {
        InputGenerator InputGenerator { get; }
        void Build(MainWindow main);
    }
}