import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { TestComponent } from './test/test.component'
import { HomeComponent } from './home/home.component'


const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "test", component: TestComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
