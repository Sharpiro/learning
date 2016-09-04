class GraphController
{
    private vertices: Vertex[];

    constructor(private context: CanvasRenderingContext2D)
    {
        this.vertices = [];
        this.vertices.push(new Vertex("A", new Rectangle(300, 200, 50, 50)));
        this.vertices.push(new Vertex("B", new Rectangle(600, 50, 50, 50)));
        this.vertices.push(new Vertex("C", new Rectangle(600, 400, 50, 50)));
        this.vertices.push(new Vertex("D", new Rectangle(900, 200, 50, 50)));
    }

    public init(): void
    {
        //for (let x of this.vertices)
        //{
        //    x.draw(this.context);
        //}

        var request = new XMLHttpRequest();
        var data: string;
        request.onload = () => data = request.responseText;
        request.open("GET", "./content/simpleGraphData.txt", false);
        request.send();
        var parser = new GraphParser(data);
        var graphData = parser.parse();
        var graphMaker = new GraphMaker(graphData.vertices, graphData.edges, graphData.type);
        var graph = graphMaker.makeAdjacencyListGraph();
        graph.adjacencyDictionary["A"].position = new Rectangle(300, 200, 50, 50);
        graph.adjacencyDictionary["B"].position = new Rectangle(600, 50, 50, 50);
        graph.adjacencyDictionary["C"].position = new Rectangle(600, 400, 50, 50);
        graph.adjacencyDictionary["D"].position = new Rectangle(900, 200, 50, 50);
        for (var nodeName in graph.adjacencyDictionary)
        {
            var node = <Vertex>graph.adjacencyDictionary[nodeName]
            console.log(node);
            node.draw(this.context);
        }
    }
}