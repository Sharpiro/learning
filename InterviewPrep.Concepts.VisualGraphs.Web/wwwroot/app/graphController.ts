class GraphController
{
    private graph: AdjacencyListGraph;
    private movingNode: Vertex;
    private frame: number;
    private offSet: number;
    private artifactDataDictionary: any;

    constructor(private context: CanvasRenderingContext2D)
    {
        this.offSet = 8;
        this.artifactDataDictionary = {};
    }

    public start()
    {
        this.init();
        this.tick(null);
    }

    private init(): void
    {
        this.registerEvents();
        var graphData = this.getWebData("./content/havocGraphData.txt");
        var artifactCost = this.getWebData("./content/artifactCost.txt");
        this.parseArtifactCostData(artifactCost);
        var parser = new GraphParser(graphData);
        var graphObject = parser.parse();
        var graphMaker = new GraphMaker(graphObject.vertices, graphObject.edges, graphObject.type);
        this.graph = graphMaker.makeAdjacencyListGraph();
        this.graph.adjacencyDictionary["A"].isActive = true;
        this.graph.adjacencyDictionary["A"].progress = this.graph.adjacencyDictionary["A"].getWeight();
    }

    private tick = (time: number): void =>
    {
        this.update();
        this.draw();
        this.createDownloadLink();
        this.frame = requestAnimationFrame(this.tick);
    }

    private update(): void
    {
        this.graph.update();
    }

    private draw(): void
    {
        this.context.clearRect(0, 0, 1200, 500);
        this.graph.draw(this.context);
        this.drawScreenData();
    }

    private parseArtifactCostData(data: string): void
    {
        var reader = new StringReader(data);
        var currentLine: string;
        while ((currentLine = reader.readLine()) != null)
        {
            var split = currentLine.split(" ");
            this.artifactDataDictionary[split[0]] = parseInt(split[1]);
        }
    }

    private drawScreenData(): void
    {
        var artifactPoints = this.graph.getFullNodes();
        var artifactPower = this.artifactDataDictionary[artifactPoints + 1]
        var totalPower = 0;
        for (var index in this.artifactDataDictionary)
        {
            if (parseInt(index) > artifactPoints)
                break;
            totalPower += <number>this.artifactDataDictionary[index]
        }
        var xPos = 50;
        var yPos = 50;
        this.context.fillStyle = "black";
        this.context.font = "25px Consolas"
        this.context.fillText("Artifact Points: ", xPos, yPos);
        this.context.fillText(artifactPoints.toString(), xPos + 225, yPos);
        this.context.fillText("Next Point Cost: ", xPos, yPos + 50);
        this.context.fillText(artifactPower.toString(), xPos + 225, yPos + 50);
        this.context.fillText("Total Power: ", xPos, yPos + 100);
        this.context.fillText(totalPower.toString(), xPos + 225, yPos + 100);
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