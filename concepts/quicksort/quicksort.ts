import { swap } from "./shared.js";

function quickSort(list: number[], lowIndex: number, highIndex: number) {
    if (lowIndex >= highIndex) return;

    const pivotIndex = partition(list, lowIndex, highIndex)
    quickSort(list, lowIndex, pivotIndex - 1)
    quickSort(list, pivotIndex + 1, highIndex)
}

function partition(list: number[], lowIndex: number, highIndex: number): number {
    const currentPivot = list[highIndex]
    let swapIndex = lowIndex
    for (let i = lowIndex; i < highIndex; i++) {
        if (list[i] < currentPivot) {
            swap(list, i, swapIndex)
            swapIndex++
        }
    }
    swap(list, swapIndex, highIndex)
    return swapIndex
}
