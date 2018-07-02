/// <reference path="./neighborColorSelector" />
/// <reference path="./pathColorSelector" />

class PathColorFactory
{
    public getDrawer(number: number): IPathColorSelector
    {
        if (number == 0)
            return new NeighborColorSelector();
        else
            return new PathColorSelector();
    }
}