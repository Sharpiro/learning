import { QuickSorter } from "./quicksorter.js"
import { getElementById } from "./shared.js"

const list = [3, 7, 8, 5, 2, 1, 9, 5, 4]
// const list = [3, 1, 8, 5, 2, 1, 9, 5, 4]
const listEl = getElementById("list")
const states = QuickSorter.sort(list)
let stateIndex = 0
writeListToDocument(states[stateIndex])
console.log(states);
console.log(`Step ${stateIndex + 1}`, states[stateIndex])

const statusEl = getElementById("status")
const lastStepEl = getElementById("lastStep")
writeStatusToDocument()

getElementById("previousButton").onmousedown = () => previous()
getElementById("nextButton").onmousedown = () => next()

/**
 * @param {KeyboardEvent} event
 */
document.body.onkeydown = (event) => {
    switch (event.key) {
        case "ArrowLeft":
            previous()
            break;
        case "ArrowRight":
            next()
            break;
    }
}

function previous() {
    if (stateIndex === 0) return

    stateIndex--
    writeListToDocument(states[stateIndex])
    writeStatusToDocument()
    console.log(`Step ${stateIndex + 1}`, states[stateIndex])
}

function next() {
    if (stateIndex === states.length - 1) return

    stateIndex++
    writeListToDocument(states[stateIndex])
    writeStatusToDocument()
    console.log(`Step ${stateIndex + 1}`, states[stateIndex])
}

function writeStatusToDocument() {
    statusEl.innerHTML = `Step: ${stateIndex + 1}/${states.length}`

    const state = states[stateIndex + 1]
    const stateType = state ? state.type : "done"
    let message = ""
    switch (stateType) {
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
        case "done":
            message = "Done"
            break;
        default: throw new Error(`add state type message: '${states[stateIndex + 1].type}'`)
    }
    lastStepEl.innerHTML = `Next Step: ${message}`
}

/**
 * @param {import("./types.js").State} state
 */
function writeListToDocument(state) {
    listEl.innerHTML = ""
    const sliceEl = document.createElement("span")
    sliceEl.classList.add("slice")
    let sliceTemp = false

    for (let i = 0; i < state.list.length; i++) {
        const item = state.list[i]
        const numberEl = document.createElement("span")
        numberEl.innerHTML = `${item}`
        const spaceEl = document.createElement("span")
        spaceEl.innerHTML = ", ".toString()
        const isInSlice = i >= state.lowIndex && i <= state.highIndex
        if (isInSlice) {
            if (!sliceTemp) {
                sliceTemp = true
                listEl.appendChild(sliceEl)
            }
            sliceEl.appendChild(numberEl)
            sliceEl.appendChild(spaceEl)
        } else {
            listEl.appendChild(numberEl)
            listEl.appendChild(spaceEl)
            continue
        }
        const isPivotIndex = i === state.pivotIndex
        if (isPivotIndex) {
            numberEl.classList.add("pivotIndex")
        }
        const isSliceIndex = i === state.sliceIndex
        if (isSliceIndex) {
            numberEl.classList.add("sliceIndex")
        }
        const isSwapIndex = i === state.swapIndex
        if (isSwapIndex) {
            numberEl.classList.add("swapIndex")
        }
        if (state.type === "loopSwap"
            && (i === state.sliceIndex || i === state.swapIndex)) {
            numberEl.classList.add("switched")
            numberEl.classList.add("switched")
        } else if (state.type === "pivotSwap"
            && (i === state.highIndex || i === state.swapIndex)) {
            numberEl.classList.add("switched")
            numberEl.classList.add("switched")
        }
    }
}
