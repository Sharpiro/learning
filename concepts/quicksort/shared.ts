export function swap(list: number[], x: number, y: number) {
    var oldX = list[x]
    list[x] = list[y]
    list[y] = oldX
}
