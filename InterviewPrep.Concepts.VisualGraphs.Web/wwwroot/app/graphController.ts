class GraphController
{
    private graph: AdjacencyListGraph;
    private movingNode: Vertex;
    private frame: number;
    private offSet: number;

    constructor(private context: CanvasRenderingContext2D)
    {
        this.offSet = 8;
    }

    public start()
    {
        this.init();
        this.tick(null);
    }

    private init(): void
    {
        this.registerEvents();
        var graphData = this.getWebData("./content/graphData.txt");
        var positionData = this.getWebData("./content/simpleGraphPositions.txt");
        var parser = new GraphParser(graphData, positionData);
        var graphObject = parser.parse();
        var graphMaker = new GraphMaker(graphObject.vertices, graphObject.edges, graphObject.type);
        this.graph = graphMaker.makeAdjacencyListGraph();
        this.graph.adjacencyDictionary["A"].activate();
    }

    private tick = (time: number): void =>
    {
        this.draw();
        this.createDownloadLink();
        this.frame = requestAnimationFrame(this.tick);
    }

    private draw(): void
    {
        this.context.clearRect(0, 0, 1200, 500);
        for (var nodeName in this.graph.adjacencyDictionary)
        {
            var node = <Vertex>this.graph.adjacencyDictionary[nodeName]
            node.draw(this.context);
        }
    }

    private registerEvents(): void
    {
        this.context.canvas.addEventListener("mousedown", (data) =>
        {
            var x = data.x - this.offSet;
            var y = data.y - this.offSet;
            for (var nodeName in this.graph.adjacencyDictionary)
            {
                var node = <Vertex>this.graph.adjacencyDictionary[nodeName]
                if (node.position.contains(x, y))
                {
                    if (data.button == 0)
                        node.activate();
                    else if (data.button == 2)
                        node.deactivate();
                    else if (data.button == 1)
                        this.movingNode = node;
                }
            }
        });
        this.context.canvas.addEventListener("mousemove", (data) =>
        {
            if (!this.movingNode) return;
            var x = data.x - this.offSet;
            var y = data.y - this.offSet;
            this.movingNode.position.x = x;
            this.movingNode.position.y = y;
        });
        this.context.canvas.addEventListener("mouseup", () =>
        {
            this.movingNode = null;
        });
    }

    private createDownloadLink()
    {
        var positions = <any[]>[];
        for (var nodeName in this.graph.adjacencyDictionary)
        {
            var node = <Vertex>this.graph.adjacencyDictionary[nodeName]
            positions.push(`${node.name} ${node.position.x} ${node.position.y}`);
        }
        var content = positions.join("\r\n");
        var elementId = "positionDownload"
        const download = <HTMLAnchorElement>document.getElementById(elementId);
        download.href = `data:text/plain,${encodeURIComponent(content)}`;
    }

    private getWebData(url: string): string
    {
        var request = new XMLHttpRequest();
        var data: string;
        request.onload = () => data = request.responseText;
        request.open("GET", url, false);
        request.send();
        return data;
    }
}