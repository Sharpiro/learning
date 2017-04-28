import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable"
import "rxjs/Rx"

@Injectable()
export class MoviesService {

  constructor(private http: Http) { }

  public observableFrom(): Observable<string> {
    let numbers = ["Star Wars", "Fight Club", "Guardians of the Galaxy"];
    let source = Observable.from(numbers);
    return source;
  }
}

export class MyObserver {
  public next(value: any) {
    console.log(`value: ${value}`);
  }

  public error(e) {
    console.log(`error: ${e}`);
  }

  public complete() {
    console.log("complete");
  }
}