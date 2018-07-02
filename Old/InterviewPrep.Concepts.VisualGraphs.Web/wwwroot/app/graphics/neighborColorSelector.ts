class NeighborColorSelector implements IPathColorSelector
{
    public selectColor(currentVertex: Vertex, currentNeighbor: Neighbor): string
    {
        var color = currentVertex.isFull() || 
            currentNeighbor.vertex.isFull() ? "#B243B6" : "black";
        return color;
    }
}