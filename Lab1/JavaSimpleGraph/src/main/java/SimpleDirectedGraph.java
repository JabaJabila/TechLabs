import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.List;

public class SimpleDirectedGraph<T>
{
    private final List<DirectedVertex<T>> _nodes = new ArrayList();

    public SimpleDirectedGraph() {}

    public SimpleDirectedGraph(List<DirectedVertex<T>> vertexes)
    {
        if (vertexes == null) throw new IllegalArgumentException("Vertex list can't be null");
        for (var vertex : vertexes)
        {
            addNode(vertex);
        }
    }

    public List<DirectedVertex<T>> getNodes()
    {
        return new ArrayList(_nodes);
    }

    public SimpleDirectedGraph<T> addNode(DirectedVertex<T> newNode)
    {
        if (newNode == null) throw new IllegalArgumentException("NewNode can't be null!");
        if (!_nodes.contains(newNode)) _nodes.add(newNode);
        return this;
    }

    public void RemoveNode(DirectedVertex<T> node)
    {
        _nodes.remove(node);
    }

    public void dfs(DirectedVertex<T> startVertex) {
        System.out.println("DFS started:");
        if (startVertex == null) throw new IllegalArgumentException("StartVertex can't be null!");
        boolean[] visited = new boolean[_nodes.size()];
        dfsWalk(startVertex, visited);
    }

    public void bfs(DirectedVertex<T> startVertex) {
        System.out.println("BFS started:");
        if (startVertex == null) throw new IllegalArgumentException("StartVertex can't be null!");
        boolean[] visited = new boolean[_nodes.size()];
        ArrayDeque<DirectedVertex<T>> queue = new ArrayDeque();
        queue.addLast(startVertex);

        while(!queue.isEmpty())
        {
            var vertex = queue.pollFirst();
            int index = _nodes.indexOf(vertex);
            if (index == -1 || visited[index]) continue;
            visited[index] = true;
            System.out.printf("Visited node %s\n", vertex.value.toString());

            for (var child : vertex.getChildren())
            {
                int ind = _nodes.indexOf(child);
                if (ind != -1 && !visited[ind])
                    queue.addLast(child);
            }
        }
    }

    private void dfsWalk(DirectedVertex<T> vertex, boolean[] visited) {
        int index = _nodes.indexOf(vertex);
        if (index == -1) return;
        visited[index] = true;
        System.out.printf("Visited node %s\n", vertex.value.toString());

        for (var child : vertex.getChildren())
        {
            int ind = _nodes.indexOf(child);
            if (ind != -1 && !visited[ind])
                dfsWalk(child, visited);
        }
    }
}
