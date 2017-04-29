import { Component, OnInit } from '@angular/core';
import { MoviesService, MyObserver } from './movies.service'

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css'],
  providers: [MoviesService]
})
export class MoviesComponent implements OnInit {

  constructor(private moviesService: MoviesService) { }
  private movies: Array<any> = [];


  ngOnInit() {
    this.getMovies();
  }

  private getMovies() {
    // this.moviesService.getMovies().subscribe(() => )
    this.moviesService.observableCreateAsync().subscribe(
      value => this.movies.push(value),
      (e: any) => console.log(`error: ${e}`),
      () => console.log("completed")
    );
  }
}