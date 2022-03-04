namespace SimpleGraphDotNET.DirectedGraph;

public class DirectedVertex<T>
{
    private readonly List<DirectedVertex<T>> _children;

    public DirectedVertex(T vertexValue)
    {
        VertexValue = vertexValue; 
        _children = new List<DirectedVertex<T>>();
    }

    public T VertexValue { get; set; }
    public IReadOnlyCollection<DirectedVertex<T>> Children => _children;

    public DirectedVertex<T> AddChild(DirectedVertex<T> vertex)
    {
        if (vertex == null) throw new ArgumentNullException(nameof(vertex));
        if (_children.Contains(vertex)) return this;
        _children.Add(vertex);
        return this;
    }
}