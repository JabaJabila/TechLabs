using SimpleGraphDotNET.DirectedGraph;

namespace Task3dotNet;
internal class Program
{
    internal static void Main()
    {
        var a = new DirectedVertex<int>(1);
        var b = new DirectedVertex<int>(2);
        var c = new DirectedVertex<int>(3);
        var d = new DirectedVertex<int>(4);
        var e = new DirectedVertex<int>(5);

        a.AddChild(b);
        b.AddChild(c);
        a.AddChild(d);
        b.AddChild(e);
        d.AddChild(e);

        var graph = new SimpleDirectedGraph<int>()
            .AddNode(a)
            .AddNode(b)
            .AddNode(c)
            .AddNode(d)
            .AddNode(e);

        graph.Dfs(a);
        graph.Bfs(a);
    }
}