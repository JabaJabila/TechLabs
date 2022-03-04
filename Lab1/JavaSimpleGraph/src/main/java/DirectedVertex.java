import java.util.ArrayList;
import java.util.List;

public class DirectedVertex<T>
{
    private final List<DirectedVertex<T>> _children;
    public T value;

    public DirectedVertex(T vertexValue)
    {
        value = vertexValue;
        _children = new ArrayList();
    }

    public List<DirectedVertex<T>> getChildren()
    {
        return new ArrayList(_children);
    }

    public DirectedVertex<T> addChild(DirectedVertex<T> vertex)
    {
        if (vertex == null) throw new IllegalArgumentException("Vertex can't be null!");
        if (_children.contains(vertex)) return this;
        _children.add(vertex);
        return this;
    }
}