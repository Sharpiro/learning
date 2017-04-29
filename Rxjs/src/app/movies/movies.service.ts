import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable"
import { Observer } from "rxjs/Rx";
// import "rxjs/add/operator/map"
// import "rxjs/add/operator/filter"
import "rxjs/Rx"
import { UtilitiesService } from "app/shared/utilities.service";

@Injectable()
export class MoviesService {
  private movies = ["Star Wars", "Fight Club", "Guardians of the Galaxy"];

  constructor(private http: Http, private utilitiesService: UtilitiesService) { }

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
    let source: Observable<string> = Observable.create(async (observer: Observer<string>) => {
      for (let movie of this.movies) {
        observer.error("whoops");
        console.log("getting movie...");
        observer.next(movie);
        await this.utilitiesService.delay(1000);
      }
      observer.complete();
    }).retryWhen(this.retryStrategy);
    // source = source.map(s => s).filter(s => true).retryWhen(this.retryStrategy);
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
    return this.http.get("assets/movies.json")
      .map(r => {
        var jObject = r.json();
        var x = [];
        x.push(jObject[0]);
        x.push(jObject[1]);
        x.push(jObject[2]);
        return x;
      });
    // return [{ name: "Star Wars" }];
  }

  private retryStrategy = (errors: Observable<any>): Observable<any> => {
    return errors
      .scan(e => (accumulator, value) => {
        console.log("scan");
        console.log(accumulator, value);
        return accumulator + 1;
      }, 0)
      // .takeWhile(acc => acc < 4)
      .delay(2000);
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

export declare function doNothing<T>(this: Observable<T>): Observable<T>;

declare module 'rxjs/Observable' {
  interface Observable<T> {
    doNothing: typeof doNothing;
  }
}