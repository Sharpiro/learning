import { Component, OnInit } from '@angular/core'
import { fs } from "../electron-node"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'electron-angular';
  test?: string

  ngOnInit(): void {
    let x: Buffer
    console.log("hi")
    console.log("hi")
    console.log("hi")

    console.log(fs)
    console.log(fs.readFileSync)
  }
}
