import JSONEditor from "jsoneditor";
import { JSONEditorMode, JSONEditorOptions } from "jsoneditor";

var leftEditorContainer = document.getElementById("leftEditorContainer");
var rightEditorContainer = document.getElementById("rightEditorContainer");
var body = document.getElementById("body");
var leftJsonEditor = new JSONEditor(leftEditorContainer, { mode: "code" });
var rightJsonEditor = new JSONEditor(rightEditorContainer, { mode: "form" });

var data = { data: 12 };
leftJsonEditor.set(data);
rightJsonEditor.set(data);

updateButton.onclick = () => {
    var newJson = leftJsonEditor.get(data);
    leftJsonEditor.set(newJson);
    rightJsonEditor.set(newJson);
}

updateTreeButton.onclick = () => {
    var newJson = rightJsonEditor.get(data);
    leftJsonEditor.set(newJson);
    rightJsonEditor.set(newJson);
}

swapToFormButton.onclick = () => {
    rightJsonEditor.setMode("form");
}

swapToTreeButton.onclick = () => {
    rightJsonEditor.setMode("tree");
}

leftEditorContainer.onkeydown = (ev) => {
    if (ev.ctrlKey && ev.keyCode === 83) {
        ev.preventDefault();
        var newJson = leftJsonEditor.get(data);
        leftJsonEditor.set(newJson);
        rightJsonEditor.set(newJson);
        console.log(ev);
    }
}

rightEditorContainer.onkeydown = (ev) => {
    if (ev.ctrlKey && ev.keyCode === 83) {
        ev.preventDefault();
        var newJson = rightJsonEditor.get(data);
        leftJsonEditor.set(newJson);
        rightJsonEditor.set(newJson);
        console.log(ev);
    }
}