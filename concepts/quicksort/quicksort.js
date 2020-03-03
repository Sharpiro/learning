import { swap } from "./functions.js";
import { State } from "./state.js";

/**
 * @typedef { import("./state.js").StateType } StateType
 */

/**
* @param {number[]} list 
*/
export function quicksort(list) {
    /** @type {State[]} */
    const states = []

    quickSortInt(list.slice(), states, 0, list.length)
    return states
}

/**
 * @param {number[]} numberList
* @param {State[]} states
* @param {number} start inclusive start index
* @param {number} end exclusive end index
*/
function quickSortInt(numberList, states, start, end) {
    if (start >= end - 1) return;

    const pivotIndex = partition(numberList, states, start, end)
    quickSortInt(numberList, states, start, pivotIndex)
    quickSortInt(numberList, states, pivotIndex + 1, end)
    states.push(new State({
        list: numberList.slice(),
        slice: numberList.slice(start, end),
        lowIndex: start,
        highIndex: end - 1,
        pivotIndex: pivotIndex,
        swapIndex: -1,
        sliceIndex: -1,
        type: "completePartition"
    }))
}

/**
 * @param {number[]} list
 * @param {State[]} states
 * @param {number} start inclusive start index
 * @param {number} end exclusive end index
 */
function partition(list, states, start, end) {
    let swapIndex = start

    pushState(states, start, end - 1, "startPartition")

    for (let i = start; i < end - 1; i++) {
        if (i !== start) {
            pushState(states, i, end - 1, "sliceIndexIncrement")
        }
        if (list[i] < list[end - 1]) {
            swap(list, i, swapIndex)
            pushState(states, i, end - 1, "loopSwap")
            swapIndex++
            pushState(states, i, end - 1, "swapIndexIncrement")
        }
    }

    swap(list, swapIndex, end - 1)
    pushState(states, -1, swapIndex, "pivotSwap")

    return swapIndex

    /**
     * @param {State[]} states
     * @param {number} loopIndex
     * @param {number} pivotIndex
     * @param {StateType} type
     */
    function pushState(states, loopIndex, pivotIndex, type) {
        states.push(new State({
            list: list.slice(),
            slice: list.slice(start, end),
            lowIndex: start,
            highIndex: end - 1,
            pivotIndex: pivotIndex,
            swapIndex: swapIndex,
            sliceIndex: loopIndex,
            type: type
        }))
    }
}
