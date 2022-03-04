public class MainJava {
    public static void main(String[] args)
    {
        var a = new DirectedVertex<Integer>(1);
        var b = new DirectedVertex<Integer>(2);
        var c = new DirectedVertex<Integer>(3);
        var d = new DirectedVertex<Integer>(4);
        var e = new DirectedVertex<Integer>(5);

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
