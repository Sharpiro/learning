/**
 * @param {number[]} list
 * @param {number} xIndex
 * @param {number} yIndex
 */
export function swap(list, xIndex, yIndex) {
    if (list[xIndex] === list[yIndex]) return
    list[xIndex] ^= list[yIndex]
    list[yIndex] ^= list[xIndex]
    list[xIndex] ^= list[yIndex]
}

/**
 * @param {string} id
 */
export function getElementById(id) {
    const val = document.getElementById(id)
    if (!val) throw new Error(`bad stuff`)
    return val
}

/**
 * @param {string} word
 * @param {string} className
 */
String.prototype.addSyntaxStyle = function (word, className) {
    const findExpression = new RegExp(`\\b${word}\\b`, "g")
    const replaceString = `<span class="${className}">${word}</span>`
    const newString = this.replace(findExpression, replaceString)
    return newString
}
