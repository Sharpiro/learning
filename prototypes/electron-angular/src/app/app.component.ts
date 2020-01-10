import { Component, OnInit } from '@angular/core'
import { remote, fs } from "../electron-node"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'electron-angular';
  test?: string

  ngOnInit(): void {
    console.log(remote.app.getAppPath())
    console.log(fs.readFileSync(".editorconfig", "utf8"))
  }
}
