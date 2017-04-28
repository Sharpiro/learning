import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MoviesComponent } from "app/movies/movies.component";

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'movies' },
    { path: "movies", component: MoviesComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }