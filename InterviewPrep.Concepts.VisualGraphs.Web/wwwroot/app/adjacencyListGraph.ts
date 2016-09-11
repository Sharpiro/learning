/// <reference path="./graphics/pathcolorfactory" />

class AdjacencyListGraph implements IGraph
{

    constructor(public adjacencyDictionary: any)
    {
    }

    public areAdjacent(firstNode: Vertex, secondNode: Vertex): boolean
    {
        return false;
    }

    public findShortestPath(vertexOne: Vertex, vertexTwo: Vertex): Vertex[]
    {
        var fringe: FringeItem[] = [];
        var tracker: any = {};
        fringe.push(new FringeItem(vertexOne, null, 0));
        //fringe.push(new FringeItem(vertexTwo, null, 500));
        //fringe.push(new FringeItem(new Vertex("z"), null, 700));
        while (fringe.length > 0)
        {
            fringe.sort((a, b) => a.distance < b.distance ? 1 : -1);
            var minFringeItem = fringe.pop();
            tracker[minFringeItem.Vertex.name] = minFringeItem;
            for (var neighbor of minFringeItem.Vertex.neighbors)
            {
                //has neighbor shortest path been found?
                if (tracker[neighbor.vertex.name] != null) continue;

                //is neighbor currently on fringe?
                var neighborFringeItem = fringe.firstOrDefault(fi => fi.Vertex == neighbor.vertex);
                //add
                var newDistance = minFringeItem.distance + neighbor.weight;
                if (neighborFringeItem == null)
                {
                    var newFringeItem = new FringeItem(neighbor.vertex, minFringeItem, newDistance);
                    fringe.push(newFringeItem);
                }
                //update
                else
                {
                    if (newDistance >= neighborFringeItem.distance) continue;
                    neighborFringeItem.distance = newDistance;
                    neighborFringeItem.LastItem = minFringeItem;
                }
            }
        }
        var temp: FringeItem;
        for (var nodeName in tracker)
        {
            var fringeItem = <FringeItem>tracker[nodeName];
            if (fringeItem.Vertex == vertexTwo)
            {
                temp = fringeItem;
                break;
            }
        }
        var path = temp.findPrevious().map(fi => fi.Vertex);
        return path;
    }

    public update(): void
    {
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            node.update();
        }
    }

    public draw(context: CanvasRenderingContext2D, number: number): void
    {
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            node.drawConnections(context, number);
        }
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

    public deactivateAll(): void
    {
        for (var nodeName in this.adjacencyDictionary)
        {
            var node = <Vertex>this.adjacencyDictionary[nodeName]
            node.deactivateFull();
        }
    }
}