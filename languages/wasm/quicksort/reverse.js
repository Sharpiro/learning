const original = [1, 2, 3, 4, 5]


// /** @param { number[] } data */
// function reverse(data) {
//     const newData = []
//     for (let i = data.length - 1; i >= 0; i--) {
//         newData.push(data[i])
//     }
//     return newData
// }

// /** @param { number[] } data */
// function reverse(data) {
//     const newData = []
//     const endIndex = data.length - 1
//     for (let i = 0; i < data.length; i++) {
//         newData.push(data[endIndex - i])
//     }
//     return newData
// }

/** @param { number[] } data */
function reverse(data) {
    const ticks = Math.floor(data.length / 2)
    const endIndex = data.length - 1
    for (let i = 0; i < ticks; i++) {
        const j = endIndex - i



        const temp = data[i]
        data[i] = data[j]
        data[j] = temp

        // console.log(i, j)
        // console.log(data)
    }
    return data
}

console.log(original)
console.log(reverse(original))
console.log(original)