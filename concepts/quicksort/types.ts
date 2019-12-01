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

export type SwapType = "startPartition" | "sliceIndexIncrement" | "swapIndexIncrement"
    | "loopSwap" | "pivotSwap" | "completePartition"
