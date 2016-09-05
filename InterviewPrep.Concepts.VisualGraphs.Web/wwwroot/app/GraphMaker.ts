class GraphMaker
{
    constructor(private vertices: any, private edges: IEdge[], private graphType: GraphType)
    {

    }

    public makeAdjacencyListGraph(): AdjacencyListGraph
    {
        for (var edge of this.edges)
        {
            this.vertices[edge.secondNode.name].neighbors.push(new Neighbor(this.vertices[edge.firstNode.name], edge.weight));
            if (this.graphType == GraphType.undirected)
                this.vertices[edge.secondNode.name].neighbors.push(new Neighbor(this.vertices[edge.firstNode.name], edge.weight));
        }
        var graph = new AdjacencyListGraph(this.vertices);
        return graph;
    }
}