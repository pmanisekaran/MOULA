import { Component, OnInit } from '@angular/core';
import { PaymentDataService as PaymentDataService } from "../payment-service.service";


@Component({
  selector: 'app-create-payment',
  templateUrl: './create-payment.component.html',
  styleUrls: ['./create-payment.component.scss']
})
export class CreatePaymentComponent implements OnInit {

  amount: number = 0;
  paymentDate: Date;
  amountErrorMsg: string = "";
  paymentDateErrorMsg = "";

  constructor(private paymentDataService: PaymentDataService) { }

  ngOnInit(): void {

    this.amountErrorMsg = "";
    this.paymentDateErrorMsg = "";

  }

  AddPayment(): void {


    if (this.paymentDate == undefined || this.paymentDate == null)
      this.paymentDateErrorMsg = "Enter a date";
    else
      this.paymentDateErrorMsg = "";

    if (this.amount == undefined || this.amount == null || this.amount <= 0)
      this.amountErrorMsg = "Enter amount greater than zero";
    else
      this.amountErrorMsg = "";
    if (this.amount > 0 && this.paymentDate != null)
      this.paymentDataService.addPayment(this.amount, this.paymentDate);
    console.log(this.amount);
    console.log(this.paymentDate);
  }

}
