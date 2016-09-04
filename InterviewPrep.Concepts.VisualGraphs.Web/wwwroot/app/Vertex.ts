class Vertex
{
    public neighbors: Neighbor[];

    constructor(public name: string, public position: Rectangle = null)
    {
        this.neighbors = [];
    }

    public draw(context: CanvasRenderingContext2D): void
    {
        var midX = this.position.x + this.position.width / 3;
        var midY = this.position.y + this.position.height / 1.5;
        context.font = "30px Consolas"
        context.strokeRect(this.position.x, this.position.y, this.position.width, this.position.height);
        context.fillText(this.name, midX, midY);
        for (var neighbor of this.neighbors)
        {
            context.moveTo(this.position.x, this.position.y);
            context.lineTo(neighbor.vertex.position.x, neighbor.vertex.position.y);
            context.stroke();
        }
    }
}