fun main()
{
    val a = DirectedVertex(1)
    val b = DirectedVertex(2)
    val c = DirectedVertex(3)
    val d = DirectedVertex(4)
    val e = DirectedVertex(5)

    a.addChild(b)
    b.addChild(c)
    a.addChild(d)
    b.addChild(e)
    d.addChild(e)

    val graph = SimpleDirectedGraph<Int>()
        .addNode(a)
        .addNode(b)
        .addNode(c)
        .addNode(d)
        .addNode(e)

    graph.dfs(a)
    graph.bfs(a)
}