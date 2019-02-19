namespace Krafi.PathFinding.Graphs.GraphAlterators
{
    public interface ITransitAdder
    {
        IGraph<T> AddTransits<T>(IGraph<T> graph) where T : INode;
    }
}