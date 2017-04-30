import { Component, OnInit } from '@angular/core';
import { MoviesService, MyObserver } from './movies.service'
import { UtilitiesService } from "app/shared/utilities.service";

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css'],
  providers: [MoviesService]
})
export class MoviesComponent implements OnInit {

  constructor(private moviesService: MoviesService, private utilitiesService: UtilitiesService) { }

  private movies: Array<any> = [];


  ngOnInit() {
    this.getMovies();
  }

  private async getMovies() {
    let subscription = this.moviesService.unsubscribeObservable().subscribe(
      value => this.movies.push(value),
      (e: any) => console.error(`error: ${e}`),
      () => console.log("completed")
    );
    await this.utilitiesService.delay(1000);
    subscription.unsubscribe();
  }
}