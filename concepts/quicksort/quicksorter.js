import { swap } from "./shared.js";

export class QuickSorter {
    /**
     * @param {number[]} originalNumberList 
     */
    static sort(originalNumberList) {
        const newNumberList = originalNumberList.slice()

        /** @type {import("./types.js").State[]} */
        const states = []
        QuickSorter.quickSortInt(newNumberList, states, 0, originalNumberList.length - 1)
        return states
    }

    /**
     * @param {number[]} numberList
     * @param {import("./types.js").State[]} states
     * @param {number[]} numberList
     * @param {number} lowIndex
     * @param {number} highIndex
     */
    static quickSortInt(numberList, states, lowIndex, highIndex) {
        if (lowIndex >= highIndex) return;

        const pivotIndex = this.partition(numberList, states, lowIndex, highIndex)
        this.quickSortInt(numberList, states, lowIndex, pivotIndex - 1)
        this.quickSortInt(numberList, states, pivotIndex + 1, highIndex)
        states.push({
            list: numberList.slice(),
            slice: numberList.slice(lowIndex, highIndex + 1),
            lowIndex: lowIndex,
            highIndex: highIndex,
            pivotIndex: pivotIndex,
            swapIndex: -1,
            sliceIndex: -1,
            type: "completePartition"
        })
    }

    /**
     * @param {number[]} list
     * @param {import("./types.js").State[]} states
     * @param {number} lowIndex
     * @param {number} highIndex
     */
    static partition(list, states, lowIndex, highIndex) {
        const currentPivot = list[highIndex]
        let swapIndex = lowIndex

        pushState(states, lowIndex, highIndex, "startPartition")

        for (let i = lowIndex; i < highIndex; i++) {
            if (i !== lowIndex) {
                pushState(states, i, highIndex, "sliceIndexIncrement")
            }
            if (list[i] < currentPivot) {
                swap(list, i, swapIndex)
                pushState(states, i, highIndex, "loopSwap")
                swapIndex++
                pushState(states, i, highIndex, "swapIndexIncrement")
            }
        }

        swap(list, swapIndex, highIndex)
        pushState(states, -1, swapIndex, "pivotSwap")

        return swapIndex

        /**
         * @param {import("./types.js").State[]} states
         * @param {number} loopIndex
         * @param {number} pivotIndex
         * @param {import("./types.js").SwapType} type
         */
        function pushState(states, loopIndex, pivotIndex, type) {
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
