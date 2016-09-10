class Vertex
{
    public neighbors: Neighbor[];
    public isActive: boolean;
    public progress: number;
    public imageUrl: string;
    public image: HTMLImageElement;
    constructor(public name: string, public position: Rectangle = null)
    {
        this.neighbors = [];
        this.progress = 0;
    }

    public update(): void
    {
        this.updateActive();
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

    public getWeight(): number
    {
        return this.neighbors[0].weight;
    }

    public activate(): void
    {
        if (this.isFull()) return;
        //this.updateActive();
        if (!this.isActive) return;
        this.progress++;
    }

    public deactivate(): void
    {
        if (this.isEmpty()) return;
        //this.updateActive();
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
        if (!this.image)
        {
            this.image = new Image();
            this.image.src = this.imageUrl;
        }
        if (this.isActive)
        {
            context.drawImage(this.image, this.position.x, this.position.y, this.position.width, this.position.height);
        }
        else
        {
            context.drawImage(this.image, this.position.x, this.position.y, this.position.width, this.position.height);
            context.fillStyle = "rgba(0, 0, 0, 0.6)";
            context.fillRect(this.position.x, this.position.y, this.position.width, this.position.height);
        }
        context.fillStyle = "black";
        context.strokeStyle = "black";
        //context.font = "30px Consolas"
        //context.fillText(this.name, midX, midY);
        context.font = "15px Consolas"
        context.fillText(`${this.progress}/${this.getWeight()}`, midX, this.position.y + this.position.height + 15);
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