class FringeItem
{
    constructor(public Vertex: Vertex, public LastItem?: FringeItem,
        public distance?: number) { }

    public findPrevious(): FringeItem[]
    {
        var list: FringeItem[] = []
        this.findPreviousRecursive(this, list);
        return list;
    }

    public findPreviousRecursive(current: FringeItem, list: FringeItem[])
    {
        if (current == null)
            return;
        list.push(current);
        this.findPreviousRecursive(current.LastItem, list);
    }
}