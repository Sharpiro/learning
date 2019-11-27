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

type SwapType = "startPartition" | "sliceIndexIncrement" | "swapIndexIncrement" | "loopSwap" | "pivotSwap" | "completePartition"

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

        // pushState(this.states, lowIndex, highIndex, "startPartition")

        const pivotIndex = this.partition(list, lowIndex, highIndex)
        this.quickSortInt(list, lowIndex, pivotIndex - 1)
        this.quickSortInt(list, pivotIndex + 1, highIndex)
        pushState(this.states, lowIndex, highIndex, "completePartition")

        function pushState(states: State[], lowIndex: number, highIndex: number, type: SwapType) {
            states.push({
                list: list.slice(),
                slice: list.slice(lowIndex, highIndex + 1),
                lowIndex: lowIndex,
                highIndex: highIndex,
                pivotIndex: pivotIndex,
                swapIndex: -1,
                sliceIndex: -1,
                type: type
            })
        }
    }

    private partition(list: number[], lowIndex: number, highIndex: number): number {
        const pivot = list[highIndex]
        let swapIndex = lowIndex

        pushState(this.states, lowIndex, highIndex, "startPartition")

        for (let i = lowIndex; i < highIndex; i++) {
            if (i !== lowIndex) {
                pushState(this.states, i, highIndex, "sliceIndexIncrement")
            }
            if (list[i] < pivot) {
                swap(list, i, swapIndex)
                pushState(this.states, i, highIndex, "loopSwap")
                swapIndex++
                pushState(this.states, i, highIndex, "swapIndexIncrement")
            }
        }

        swap(list, swapIndex, highIndex)
        pushState(this.states, -1, swapIndex, "pivotSwap")

        return swapIndex

        function pushState(states: State[], loopIndex: number, pivotIndex: number, type: SwapType) {
            states.push({
                list: list.slice(),
                slice: list.slice(lowIndex, highIndex + 1),
                lowIndex: lowIndex,
                highIndex: highIndex,
                pivotIndex: pivotIndex,
                swapIndex: swapIndex,
                sliceIndex: loopIndex,
                type: type
            })
        }
    }
}