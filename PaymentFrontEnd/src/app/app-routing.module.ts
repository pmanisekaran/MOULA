import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{UsersComponent} from "./users/users.component";
import{PaymentsComponent} from "./payments/payments.component";
import{AddUserComponent} from "./add-user/add-user.component";
import{ListUsersComponent} from "./list-users/list-users.component";



const routes: Routes = [
{path:'', component:UsersComponent},
{path:'payments', component:PaymentsComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
