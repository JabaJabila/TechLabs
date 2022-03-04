open SimpleGraphDotNET.DirectedGraph

let addChild (child: DirectedVertex<int>) (parent : DirectedVertex<int>) =
    parent.AddChild(child)

let addNode (node : DirectedVertex<int>) (graph : SimpleDirectedGraph<int>) =
    graph.AddNode(node)


let e = DirectedVertex<int> 5
let d = DirectedVertex<int> 4 |> addChild e
let c = DirectedVertex<int> 3
let b = DirectedVertex<int> 2 |> addChild c |> addChild e
let a = DirectedVertex<int> 1 |> addChild b |> addChild d


let graph = SimpleDirectedGraph<int>() |> addNode a |> addNode b |> addNode c |> addNode d |> addNode e

graph.Dfs(a);
graph.Bfs(a);