Array.prototype.firstOrDefault = function <T>(aFunction: (item: T) => boolean)
{
    var temp = this.length;
    var filteredList = this.filter(aFunction);
    if (filteredList.length == 0)
        return null;
    return filteredList[0];
};

interface Array<T>
{
    firstOrDefault(aFunction: (item: T) => boolean): T;
}