import { Component, OnInit } from '@angular/core'
import { remote, fs, path } from "../electron-node"

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
    const testFilePath = path.join(remote.app.getAppPath(), 'icon.png')
    console.log("test file exists:", fs.existsSync(testFilePath))
  }
}
