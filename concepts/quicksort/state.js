/**
 * @typedef {
    undefined |
    "startPartition"|
    "sliceIndexIncrement" |
    "swapIndexIncrement"| 
    "loopSwap" |
    "pivotSwap" |
    "completePartition"
} StateType
*/

export class State {
    /** @type {number[]} */
    list = []
    /** @type {number[]} */
    slice = []
    /** @type {number} */
    lowIndex = 0
    /** @type {number} */
    highIndex = 0
    /** @type {number} */
    pivotIndex = 0
    /** @type {number} */
    swapIndex = 0
    /** @type {number} */
    sliceIndex = 0
    /** @type {StateType} */
    type

    /**
     * @param {Partial<State>} init
     */
    constructor(init) {
        Object.assign(this, init)
    }

    getStateMessage() {
        let message = undefined
        switch (this.type) {
            case "startPartition":
                message = "Start Partition"
                break;
            case "sliceIndexIncrement":
                message = "Increment loop index"
                break;
            case "swapIndexIncrement":
                message = "Increment swap index"
                break;
            case "loopSwap":
                message = "Swap loop index and swap index"
                break;
            case "pivotSwap":
                message = "Swap pivot index and swap index"
                break;
            case "completePartition":
                message = "Complete Partition"
                break;
            default: throw new Error(`invalid state type '${this.type}'`)
        }

        return message
    }
}
