import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PaymentsComponent } from './payments/payments.component';
import { HttpClientModule } from '@angular/common/http';
import { CreatePaymentComponent } from './create-payment/create-payment.component';
import { ListPaymentsComponent } from './list-payments/list-payments.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PaymentsComponent,
    CreatePaymentComponent,
    ListPaymentsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
