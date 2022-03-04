public class Main {
    public static void main(String[] args) {
        DirectedVertex<Integer> a = new DirectedVertex(1);
        DirectedVertex<Integer> b = new DirectedVertex(2);
        DirectedVertex<Integer> c = new DirectedVertex(3);
        DirectedVertex<Integer> d = new DirectedVertex(4);
        DirectedVertex<Integer> e = new DirectedVertex(5);

        a.addChild(b);
        b.addChild(c);
        a.addChild(d);
        b.addChild(e);
        d.addChild(e);

        SimpleDirectedGraph<Integer> graph = new SimpleDirectedGraph<Integer>()
                .addNode(a)
                .addNode(b)
                .addNode(c)
                .addNode(d)
                .addNode(e);

        graph.dfs(a);
        graph.bfs(a);
    }
}
