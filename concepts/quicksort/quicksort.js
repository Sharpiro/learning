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

    quickSortInt(list.slice(), states, 0, list.length - 1)
    return states
}

/**
* @param {number[]} numberList
* @param {State[]} states
* @param {number[]} numberList
* @param {number} lowIndex
* @param {number} highIndex
*/
function quickSortInt(numberList, states, lowIndex, highIndex) {
    if (lowIndex >= highIndex) return;

    const pivotIndex = partition(numberList, states, lowIndex, highIndex)
    quickSortInt(numberList, states, lowIndex, pivotIndex - 1)
    quickSortInt(numberList, states, pivotIndex + 1, highIndex)
    states.push(new State({
        list: numberList.slice(),
        slice: numberList.slice(lowIndex, highIndex + 1),
        lowIndex: lowIndex,
        highIndex: highIndex,
        pivotIndex: pivotIndex,
        swapIndex: -1,
        sliceIndex: -1,
        type: "completePartition"
    }))
}

/**
 * @param {number[]} list
 * @param {State[]} states
 * @param {number} lowIndex
 * @param {number} highIndex
 */
function partition(list, states, lowIndex, highIndex) {
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
     * @param {State[]} states
     * @param {number} loopIndex
     * @param {number} pivotIndex
     * @param {StateType} type
     */
    function pushState(states, loopIndex, pivotIndex, type) {
        states.push(new State({
            list: list.slice(),
            slice: list.slice(lowIndex, highIndex + 1),
            lowIndex: lowIndex,
            highIndex: highIndex,
            pivotIndex: pivotIndex,
            swapIndex: swapIndex,
            sliceIndex: loopIndex,
            type: type
        }))
    }
}
