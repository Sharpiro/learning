class AdjacencyListGraph implements IGraph
{
    constructor(public adjacencyDictionary: any)
    {

    }
    public areAdjacent(firstNode: Vertex, secondNode: Vertex): boolean
    {
        return false;
    }
}