namespace Krafi.PathFinding.Graphs.GraphAlterators
{
    public interface ITransitAdder
    {
        IGraph AddTransits(IGraph graph);
    }
}