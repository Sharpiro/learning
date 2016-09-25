/// <reference path="./interfaces" />
/// <reference path="./rectangle" />

class GraphParser
{
    private graphDataReader: StringReader;
    //private graphPositionReader: StringReader;

    constructor(graphData: string)
    {
        this.graphDataReader = new StringReader(graphData);
    }

    public parse(): IGraphData
    {
        var numberOfNodes = parseInt(this.graphDataReader.readLine());
        var graphType = <GraphType>parseInt(GraphType[<any>this.graphDataReader.readLine().toLowerCase()]);
        var vertices: any = {};
        for (let i = 0; i < numberOfNodes; i++)
        {
            var currentLine = this.graphDataReader.readLine().split("\t");
            var name = currentLine[0];
            vertices[name] = new Vertex(name);
            vertices[name].position = new Rectangle(parseInt(currentLine[1]), parseInt(currentLine[2]), 50, 50);
            if (currentLine[3])
                vertices[name].imageUrl = `./content/images/havoc/${currentLine[3]}`;
            if (currentLine[4])
                vertices[name].tooltip = currentLine[4];
        }

        var edges: IEdge[] = [];
        var line: string;
        while ((line = this.graphDataReader.readLine()) != null)
        {
            var splitData = line.split("\t");
            edges.push(
                {
                    firstNode: new Vertex(splitData[0]),
                    secondNode: new Vertex(splitData[1]),
                    weight: parseInt(splitData[2])
                });
        }
        return { edges: edges, vertices: vertices, type: graphType };
    }


}