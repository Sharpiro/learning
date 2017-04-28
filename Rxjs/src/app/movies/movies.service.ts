import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable"
import { Observer } from "rxjs/Rx";
// import "rxjs/add/operator/map"
// import "rxjs/add/operator/filter"
import "rxjs/Rx"

@Injectable()
export class MoviesService {
  private movies = ["Star Wars", "Fight Club", "Guardians of the Galaxy"];

  constructor(private http: Http) { }

  public observableFrom(): Observable<string> {
    let source = Observable.from(this.movies);
    return source;
  }

  public observableCreateSync(): Observable<string> {
    let source = Observable.create((observer: Observer<string>) => {
      for (let i of this.movies) {
        // if (i == "Fight Club") observer.error("something went wrong");
        observer.next(i);
      }
      observer.complete();
    });
    return source;
  }

  public observableCreateAsync(): Observable<string> {
    let source: Observable<string> = Observable.create((observer: Observer<string>) => {
      let index = 0;
      let produceValue = () => {
        observer.next(this.movies[index++]);
        if (index < this.movies.length) {
          setTimeout(produceValue, 2000);
        }
        else
          observer.complete();
      };
      produceValue();
    });
    source.map(s => s).filter(s => true);
    return source;
  }

  public createFromEvent() {
    let source = Observable.fromEvent(document, "mousemove")
      .map((e: MouseEvent) => {
        return { x: e.clientX, y: e.clientY }
      })
      .filter(e => e.x < 500)
      .delay(300);
    return source;
  }

  public getMovies() {
    return Observable.from(this.movies)
    // return [{ name: "Star Wars" }];
  }
}

export class MyObserver implements Observer<string>{
  public next(value: string) {
    console.log(`value: ${value}`);
  }

  public error(e: any) {
    console.log(`error: ${e}`);
  }

  public complete() {
    console.log("complete");
  }
}