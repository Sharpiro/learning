/// <reference path="./GraphController" />
var canvas = document.createElement('canvas');
canvas.id = "canvas";
canvas.width = 1200;
canvas.height = 500;
canvas.style.zIndex = "8";
canvas.style.position = "absolute";
canvas.style.border = "1px solid";
canvas.oncontextmenu = () => false;
var body = document.getElementsByTagName("body")[0];
body.appendChild(canvas);
var context = canvas.getContext("2d");

var graphController = new GraphController(context);
graphController.start();
