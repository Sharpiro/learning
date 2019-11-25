import { swap } from "./shared.js";

export interface State {
    list: number[]
    slice: number[]
    lowIndex: number
    highIndex: number
    pivotIndex: number
    swapIndex: number
    sliceIndex: number
    type: SwapType
}

type SwapType = "sliceIndexIncrement" | "swapIndexIncrement" | "loopSwap" | "pivotSwap"

export class QuickSorter {
    readonly maxLength = 9
    states: State[] = []

    constructor(private originalList: number[]) {
        if (originalList.length > this.maxLength) {
            throw new Error(`Array max length is '${this.maxLength}'`)
        }
    }

    build() {
        this.quickSortInt(this.originalList.slice(), 0, this.originalList.length - 1)
    }

    private quickSortInt(list: number[], lowIndex: number, highIndex: number) {
        if (lowIndex >= highIndex) return;

        const pivotIndex = this.partition(list, lowIndex, highIndex)
        this.quickSortInt(list, lowIndex, pivotIndex - 1)
        this.quickSortInt(list, pivotIndex + 1, highIndex)
    }

    private partition(list: number[], lowIndex: number, highIndex: number): number {
        const pivot = list[highIndex]
        let swapIndex = lowIndex

        for (let i = lowIndex; i < highIndex; i++) {
            pushState(this.states, highIndex, i, "sliceIndexIncrement")
            if (list[i] < pivot) {
                swap(list, i, swapIndex)
                pushState(this.states, highIndex, i, "loopSwap")
                swapIndex++
                pushState(this.states, highIndex, i, "swapIndexIncrement")
            }
        }

        pushState(this.states, highIndex, highIndex, "sliceIndexIncrement")
        swap(list, swapIndex, highIndex)
        pushState(this.states, swapIndex, highIndex, "pivotSwap")

        return swapIndex

        function pushState(states: State[], pivotIndex: number, partitionIndex: number, type: SwapType) {
            states.push({
                list: list.slice(),
                slice: list.slice(lowIndex, highIndex + 1),
                lowIndex: lowIndex,
                highIndex: highIndex,
                pivotIndex: pivotIndex,
                swapIndex: swapIndex,
                sliceIndex: partitionIndex,
                type: type
            })
        }
    }
}