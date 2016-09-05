class GraphParser
{
    private graphDataReader: StringReader;
    private graphPositionReader: StringReader;

    constructor(graphData: string, positionData: string)
    {
        this.graphDataReader = new StringReader(graphData);
        this.graphPositionReader = new StringReader(positionData);
    }

    public parse(): IGraphData
    {
        var numberOfNodes = parseInt(this.graphDataReader.readLine());
        var graphType = <GraphType>parseInt(GraphType[<any>this.graphDataReader.readLine().toLowerCase()]);
        var vertices: any = {};
        for (let i = 0; i < numberOfNodes; i++)
        {
            var name = this.graphDataReader.readLine();
            var positionData = this.graphPositionReader.readLine().split(" ");
            if (name != positionData[0])
                throw new Error("data mismatch between graph data and position data");
            vertices[name] = new Vertex(name);
            vertices[name].position = new Rectangle(parseInt(positionData[1]), parseInt(positionData[2]), 50, 50);
        }

        var edges: IEdge[] = [];
        var line: string;
        while ((line = this.graphDataReader.readLine()) != null)
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


}