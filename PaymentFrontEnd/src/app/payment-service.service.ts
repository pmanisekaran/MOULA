import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PaymentDataService {

  
  constructor(private http: HttpClient) { }

  public addPayment(paymentAmount: number, pDate: Date) {


    console.log("calling from addpayment");

    var createPaymentRequestObject = {

      amount: paymentAmount,
      paymentDate: pDate
    }
    var json= JSON.stringify(createPaymentRequestObject);


    console.log("call starting ");
    this.http.post("http://localhost:60770/payment/createpayment",createPaymentRequestObject).subscribe(data => {

      console.log(data);
    });

  }


}
