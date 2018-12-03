import { Component } from '@angular/core';
// var temp = require("fs")
import { readFile } from 'fs';

console.log(readFile)

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'c';
}
