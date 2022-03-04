namespace SimpleGraphDotNET.DirectedGraph;

public class SimpleDirectedGraph<T>
{
    private readonly List<DirectedVertex<T>> _nodes = new List<DirectedVertex<T>>();

    public SimpleDirectedGraph() { }

    public SimpleDirectedGraph(List<DirectedVertex<T>> vertexes)
    { 
        if (vertexes == null) throw new ArgumentNullException(nameof(vertexes));
        vertexes.ForEach(v => AddNode(v));
    }

    public IReadOnlyCollection<DirectedVertex<T>> Nodes => _nodes;

    public SimpleDirectedGraph<T> AddNode(DirectedVertex<T> newNode)
    {
        if (newNode == null) throw new ArgumentNullException(nameof(newNode));
        if (!_nodes.Contains(newNode)) _nodes.Add(newNode);
        return this;
    }

    public void RemoveNode(DirectedVertex<T> node)
    {
        _nodes.Remove(node);
    }

    public void Dfs(DirectedVertex<T> startVertex)
    {
        Console.WriteLine("DFS started:");
        if (startVertex == null) throw new ArgumentNullException(nameof(startVertex));
        var visited = new bool[_nodes.Count];
        DfsWalk(startVertex, visited);
    }

    public void Bfs(DirectedVertex<T> startVertex)
    {
        Console.WriteLine("BFS started:");
        if (startVertex == null) throw new ArgumentNullException(nameof(startVertex));
        var visited = new bool[_nodes.Count];
        var queue = new Queue<DirectedVertex<T>>();
        queue.Enqueue(startVertex);

        while (queue.Count != 0)
        {
            var vertex = queue.Dequeue();
            int index = _nodes.IndexOf(vertex);
            if (index == -1 || visited[index]) continue;
            visited[index] = true;
            Console.WriteLine($"Visited node {vertex.VertexValue}");

            foreach (var child in vertex.Children)
            {
                int ind = _nodes.IndexOf(child);
                if (ind != -1 && !visited[ind])
                    queue.Enqueue(child);
            }
        }
    }

    private void DfsWalk(DirectedVertex<T> vertex, bool[] visited)
    {
        int index = _nodes.IndexOf(vertex);
        if (index == -1) return;
        visited[index] = true;
        Console.WriteLine($"Visited node {vertex.VertexValue}");

        foreach (var child in vertex.Children) 
        { 
            int ind = _nodes.IndexOf(child); 
            if (ind != -1 && !visited[ind]) 
                DfsWalk(child, visited);
        }
    }
}
