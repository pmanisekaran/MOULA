import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreatePaymentComponent} from "./create-payment/create-payment.component" ;
import {ListPaymentsComponent} from "./list-payments/list-payments.component" ;
import{PaymentsComponent} from "./payments/payments.component";
 



const routes: Routes = [

{path:'', component:PaymentsComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
