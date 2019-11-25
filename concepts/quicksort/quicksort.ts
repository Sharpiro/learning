import { swap } from "./shared.js";

export function quickSort(list: number[]) {
    const newList = list.slice()
    quickSortInt(newList, 0, list.length - 1)
    return newList
}

function quickSortInt(list: number[], lowIndex: number, highIndex: number) {
    if (lowIndex >= highIndex) return;

    const pivotIndex = partition(list, lowIndex, highIndex)
    quickSortInt(list, lowIndex, pivotIndex - 1)
    quickSortInt(list, pivotIndex + 1, highIndex)
}

function partition(list: number[], lowIndex: number, highIndex: number): number {
    const pivot = list[highIndex]

    let nextPivotIndex = lowIndex

    for (let i = lowIndex; i < highIndex; i++) {
        if (list[i] < pivot) {
            swap(list, i, nextPivotIndex)
            nextPivotIndex++
        }
    }
    swap(list, nextPivotIndex, highIndex)

    return nextPivotIndex
}

