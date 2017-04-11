import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from "app/admin/admin.component";
import { UsersComponent } from "app/admin/users/users.component";
import { EmailBlastComponent } from "app/admin/email-blast/email-blast.component";

const routes: Routes = [
  {
    path: "admin", component: AdminComponent, children: [
      { path: "", component: UsersComponent },
      { path: "blast", component: EmailBlastComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
