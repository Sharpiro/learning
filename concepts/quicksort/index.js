import { quicksort } from "./quicksort.js"
import { getElementById } from "./functions.js"
import { State } from "./state.js"

/**
 * @typedef { import("./state.js").StateType } StateType
 */

const initialList = [3, 7, 8, 5, 2, 1, 9, 5, 4]
/** @type {State[]} */
let states = []
let stateIndex = 0

const listEl = getElementById("list")
const statusEl = getElementById("status")
const currentStepEl = getElementById("currentStep")
const nextStepEl = getElementById("nextStep")
getElementById("previousButton").onmousedown = () => previous()
getElementById("nextButton").onmousedown = () => next()
getElementById("updateListButton").onmousedown = () => updateList()

updateSortStates(initialList)
writeStatusToDocument()

function updateList() {
    const promptResult = prompt("Enter valid json list of numbers.  Ex: [1,2]")
    if (!promptResult) return

    try {
        /** @type {number[]} */
        const numberList = JSON.parse(promptResult)
        if (!Array.isArray(numberList)) {
            throw new Error("Could not parse array")
        }
        if (numberList.length < 2) {
            throw new Error("Array length must be at least 2")
        }
        if (numberList.length > 620){
            alert("enjoy DDOS-ing yourself")
        }
        console.log(numberList)
        updateSortStates(numberList)
        writeStatusToDocument()
    }
    catch (err) {
        console.error(err)
        alert(err)
    }
}

/**
 * 
 * @param {number[]} list 
 */
function updateSortStates(list) {
    states = quicksort(list)
    stateIndex = 0
    writeListToDocument(states[stateIndex])
    highlightSyntax()
    console.log(states);
    console.log(`Step ${stateIndex + 1}`, states[stateIndex])
}

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

function highlightSyntax() {
    const referenceCodeElement = document.getElementById("referenceCode")
    if (!referenceCodeElement) throw new Error("Couldn't find reference code element")
    referenceCodeElement.innerHTML = referenceCodeElement.innerHTML
        .addSyntaxStyle("function", "keyword-highlight")
        .addSyntaxStyle("const", "keyword-highlight")
        .addSyntaxStyle("let", "keyword-highlight")
        .addSyntaxStyle("for", "keyword-highlight")
        .addSyntaxStyle("return", "keyword-highlight")
        .addSyntaxStyle("number", "type-highlight")
        .addSyntaxStyle("quickSort", "func-identifier-highlight")
        .addSyntaxStyle("partition", "func-identifier-highlight")
        .addSyntaxStyle("swap", "func-identifier-highlight")
        .addSyntaxStyle("1", "func-identifier-highlight")
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

    const currentState = states[stateIndex]
    const currentStateMessage = currentState ? currentState.getStateMessage() : "None"
    currentStepEl.innerHTML = `${currentStateMessage}`

    const nextState = states[stateIndex + 1]
    const nextStateMessage = nextState ? nextState.getStateMessage() : "None"
    nextStepEl.innerHTML = `${nextStateMessage}`
}

/**
 * @param {State} state
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
