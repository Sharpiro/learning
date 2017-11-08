import JSONEditor from "jsoneditor";

var leftJsonEditor = new JSONEditor(leftEditorContainer, { mode: "code" });
var rightJsonEditor = new JSONEditor(rightEditorContainer, { mode: "form" });

var data = { data: 12 };
leftJsonEditor.set(data);
rightJsonEditor.set(data);

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
            var newJson = leftJsonEditor.get(data);
            rightJsonEditor.set(newJson);
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
            var newJson = rightJsonEditor.get(data);
            leftJsonEditor.set(newJson);
        }
        catch (e) {
            console.log(e);
            alert(e)
        }
    }
}