import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  private users: any[];
  private hasPermission = true;

  constructor() { }

  async ngOnInit() {
    if (this.hasPermission) {
      var users = await this.getUsers();
    }
    else {
      this.users = [];
    }
  }

  private async getUsers() {
    try {
      console.log("getting users");
      await this.delay(2000);
      var xyz = true;
      if (xyz)
        throw "oh geez";
      console.log("got users");
      return [
        { name: "john", email: "john@angular.com" },
        { name: "dave", email: "dave@angular.com" }
      ];
    }
    catch (e) {
      console.error("Error trying to get users");
      console.error(e);
    }
  }

  private delay(ms: number): Promise<void> {
    return new Promise<void>((x) => setTimeout(x, ms));
  }
}
