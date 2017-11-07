// import JSONEditor from "jsoneditor";
// import { JSONEditorMode, JSONEditorOptions } from "jsoneditor";

var codeContainer = document.getElementById("jsonEditorCode");
var viewContainer = document.getElementById("jsonEditorView");
var jsonEditorCode = new JSONEditor(codeContainer, { mode: "code" });
var jsonEditorView = new JSONEditor(viewContainer, { mode: "view" });

var data = { data: 12 };
jsonEditorCode.set(data);
jsonEditorView.set(data);

updateButton.onclick = () => {
    var newJson = jsonEditorCode.get(data);
    jsonEditorCode.set(newJson);
    jsonEditorView.set(newJson);
}