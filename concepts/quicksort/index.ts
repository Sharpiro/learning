import { QuickSorter, State } from "./quickSorter.js"

const list = [3, 7, 8, 5, 2, 1, 9, 5, 4]
const listEl = document.getElementById("list") as HTMLElement
const quickSorter = new QuickSorter(list)
quickSorter.build()
const states = quickSorter.states
let stateIndex = 0
writeListToDocument(states[stateIndex])
console.log(states);

const statusEl = document.getElementById("status") as HTMLElement
writeStatusToDocument()

const previousButtonEl = document.getElementById("previousButton") as HTMLButtonElement
previousButtonEl.onmousedown = () => previous()

const nextButtonEl = document.getElementById("nextButton") as HTMLButtonElement
nextButtonEl.onmousedown = () => next()

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
    console.log(states[stateIndex].type)
}

function next() {
    if (stateIndex === states.length - 1) return

    stateIndex++
    writeListToDocument(states[stateIndex])
    writeStatusToDocument()
    console.log(states[stateIndex].type)
}

function writeStatusToDocument() {
    statusEl.innerHTML = `Step: ${stateIndex + 1}/${states.length}`
}

function writeListToDocument(state: State) {
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



        // if (i + 1 !== state.list.length) {
        //     const spaceEl = document.createElement("span")
        //     spaceEl.innerHTML = ", ".toString()
        //     if (isInSlice) {
        //         spaceEl.classList.add("slice")
        //     }
        //     listEl.appendChild(spaceEl)
        // }
    }
    // listEl.appendChild(sliceEl)
}
