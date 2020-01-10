const { remote, shell } = require('electron')
const { Menu, MenuItem } = remote

const menu = new Menu()
menu.append(new MenuItem({ label: 'Open Containing Folder', click: openContainingFolder }))
menu.append(new MenuItem({ type: 'separator' }))
menu.append(new MenuItem({ label: 'Inspect Element', click: inspectElement }))
// menu.append(new MenuItem({ label: 'MenuItem2', type: 'checkbox', checked: true }))
// menu.append(new MenuItem({ type: 'separator' }))

/** @type {{ x: number; y: number; } | null} */
let rightClickPosition = null

window.addEventListener('contextmenu', (event) => {
  event.preventDefault()
  rightClickPosition = { x: event.x, y: event.y }
  menu.popup({ window: remote.getCurrentWindow() })
}, false)

function inspectElement() {
  if (!rightClickPosition) throw new Error("error retrieving click position")
  const x = rightClickPosition.x
  const y = rightClickPosition.y
  remote.getCurrentWindow().webContents.inspectElement(x, y)
}

function openContainingFolder() {
  shell.openItem(".")
}
