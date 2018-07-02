import JSONEditor from "jsoneditor";

var leftJsonEditor = new JSONEditor(leftEditorContainer, { mode: "code" });
var rightJsonEditor = new JSONEditor(rightEditorContainer, { mode: "form" });

const storageKey = "data";
var storageJsonText = localStorage.getItem(storageKey);
var defaultJson = { data: 12 };
if (storageJsonText)
    var initialJson = JSON.parse(storageJsonText);
else
    var initialJson = defaultJson;
leftJsonEditor.set(initialJson);
rightJsonEditor.set(initialJson);

leftEditorMode.onchange = (e) => {
    if (e.target.value === "code")
        leftJsonEditor.setMode("code");
    else if (e.target.value === "text")
        leftJsonEditor.setMode("text");
    else
        console.error("invalid mode selected");
}

rightEditorMode.onchange = (e) => {
    if (e.target.value === "tree")
        rightJsonEditor.setMode("tree");
    else if (e.target.value === "form")
        rightJsonEditor.setMode("form");
    else
        console.error("invalid mode selected");
}

leftEditorContainer.onkeydown = (ev) => {
    if (ev.ctrlKey && ev.keyCode === 83) {
        ev.preventDefault();
        try {
            var newJson = leftJsonEditor.get();
            rightJsonEditor.set(newJson);
            var jsonText = JSON.stringify(newJson);
            localStorage.setItem(storageKey, jsonText);
        }
        catch (e) {
            console.log(e);
            alert(e)
        }
    }
}

rightEditorContainer.onkeydown = (ev) => {
    if (ev.ctrlKey && ev.keyCode === 83) {
        ev.preventDefault();
        try {
            var newJson = rightJsonEditor.get();
            leftJsonEditor.set(newJson);
            var jsonText = JSON.stringify(newJson);
            localStorage.setItem(storageKey, jsonText);
        }
        catch (e) {
            console.log(e);
            alert(e)
        }
    }
}

window.onkeydown = (ev) => {
    if (ev.ctrlKey && ev.shiftKey && ev.keyCode == 82) {
        ev.preventDefault();
        leftJsonEditor.set(defaultJson);
        rightJsonEditor.set(defaultJson);
        localStorage.removeItem(storageKey);
    }
}