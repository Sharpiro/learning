class Rectangle
{
    constructor(public x: number, public y: number, public width: number, public height: number)
    {

    }

    public contains(x: number, y: number): boolean
    {
        if (x > this.x && x < this.x + this.width)
        {
            if (y > this.y && y < this.y + this.height)
            {
                return true;
            }
        }
        return false;
    }
}