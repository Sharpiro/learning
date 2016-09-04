interface IEdge
{
    firstNode: Vertex;
    secondNode: Vertex;
    weight: number
}

interface IGraph
{
    areAdjacent(firstNode: Vertex, secondNode: Vertex): boolean
}

interface IGraphData
{
    edges: IEdge[];
    vertices: any;
    type: GraphType;
}

enum GraphType { none, directed, undirected }