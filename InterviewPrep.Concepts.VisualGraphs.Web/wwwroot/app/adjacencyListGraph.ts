class AdjacencyListGraph implements IGraph
{
    constructor(public adjacencyDictionary: any)
    {

    }
    public areAdjacent(firstNode: Vertex, secondNode: Vertex): boolean
    {
        return false;
    }

    public update(): void
    {
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            node.update();
        }
    }

    public draw(context: CanvasRenderingContext2D): void
    {
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            node.draw(context);
        }
    }

    public getFullNodes(): number
    {
        var sum = 0;
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            sum += node.progress
        }
        return sum - 1;
    }
}