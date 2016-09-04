class GraphController
{
    private vertices: Vertex[];

    constructor(private context: CanvasRenderingContext2D)
    {
        this.vertices = [];
        this.vertices.push(new Vertex("A", new Rectangle(300, 200,50,50)));
        this.vertices.push(new Vertex("B", new Rectangle(600, 50, 50, 50)));
        this.vertices.push(new Vertex("C", new Rectangle(600, 400, 50, 50)));
        this.vertices.push(new Vertex("D", new Rectangle(900, 200, 50, 50)));
    }

    public init(): void
    {
        for (var x of this.vertices)
        {
            x.draw(this.context);
        }
    }
}