fetch("quicksort.wat").then(res => {
  res.text().then(watCode => {
    const sourceCodeEl = document.getElementById("sourceCode")
    if (!sourceCodeEl) throw new Error("no source div")
    const codeEl = document.createElement("code")
    codeEl.innerHTML = watCode
    sourceCodeEl.appendChild(codeEl)
    const commandCount = calculateCommandCount(watCode)
    console.log("commandCount:", commandCount)
    const commandCountEl = document.getElementById("commandCountId")
    if (!commandCountEl) throw new Error("no command count el")
    commandCountEl.innerHTML = commandCount.toString()
  })
})

/** @param { string } data */
function calculateCommandCount(data) {
  const pattern = /^ *i32\.|^ *local\.|^ *br_if|^ *if|^ *loop|^ *call /gm
  const matches = data.match(pattern)
  if (!matches) throw new Error("bad regex")
  return matches.length
}
