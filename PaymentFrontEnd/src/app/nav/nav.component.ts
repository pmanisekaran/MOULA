import { Component, OnInit } from '@angular/core';
import {CreatePaymentComponent} from "../create-payment/create-payment.component" ;
import {ListPaymentsComponent} from "../list-payments/list-payments.component" ;

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  appTitle: string="Moula Test App"
  constructor() { }

  ngOnInit(): void {
  }

}
