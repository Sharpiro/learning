class GraphParser
{
    private lines: string[];
    private index: number;

    constructor(private data: string)
    {
        this.lines = this.data.split("\r\n");
    }

    public parse(): IGraphData
    {
        this.index = 0;
        var numberOfNodes = parseInt(this.readLine());
        var graphType = <GraphType>parseInt(GraphType[<any>this.readLine().toLowerCase()]);
        var vertices: any = {};
        for (let i = 0; i < numberOfNodes; i++)
        {
            var name = this.readLine();
            vertices[name] = new Vertex(name);
        }

        var edges: IEdge[] = [];
        var line: string;
        while ((line = this.readLine()) != null)
        {
            var splitData = line.split(" ");
            edges.push(
                {
                    firstNode: new Vertex(splitData[0]),
                    secondNode: new Vertex(splitData[1]),
                    weight: parseInt(splitData[2])
                });
        }
        return { edges: edges, vertices: vertices, type: graphType};
    }

    private readLine(): string
    {
        if (this.index >= this.lines.length)
            return null;
        var line = this.lines[this.index];
        this.index++;
        return line;
    }
}