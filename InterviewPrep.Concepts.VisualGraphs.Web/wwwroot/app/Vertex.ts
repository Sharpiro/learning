class Vertex
{
    public neighbors: Neighbor[];
    private isActive: boolean;
    private progress: number;

    constructor(public name: string, public position: Rectangle = null)
    {
        this.neighbors = [];
    }

    public updateActive(): void
    {
        for (var neighbor of this.neighbors)
        {
            if (neighbor.vertex.isFull())
            {
                this.isActive = true;
                break;
            }
        }
    }

    public activate(): void
    {
        if (this.isFull()) return;
        this.updateActive();
        this.progress++;
    }

    public deactivate(): void
    {
        if (this.isEmpty()) return;
        this.updateActive();
        this.progress--;
    }

    public isFull(): boolean
    {
        return this.isActive && this.progress == this.neighbors[0].weight;
    }

    public isEmpty(): boolean
    {
        return this.isActive && this.progress == 0;
    }

    public draw(context: CanvasRenderingContext2D): void
    {
        var midX = this.position.x + this.position.width / 3;
        var midY = this.position.y + this.position.height / 1.5;
        if (this.isActive)
        {
            context.fillStyle = "yellow";
            context.fillRect(this.position.x, this.position.y, this.position.width, this.position.height);
        }
        else
        {
            context.strokeRect(this.position.x, this.position.y, this.position.width, this.position.height);
        }
        context.fillStyle = "black";
        context.strokeStyle = "black";
        context.font = "30px Consolas"
        context.fillText(this.name, midX, midY);
        for (var neighbor of this.neighbors)
        {
            context.beginPath();
            context.moveTo(this.position.x, this.position.y);
            context.lineTo(neighbor.vertex.position.x, neighbor.vertex.position.y);
            context.stroke();
            context.closePath();
        }
    }
}