import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 

@Injectable({
  providedIn: 'root'
})
export class UserDataServiceService {

  constructor(private http: HttpClient) { }

  addUserData(userName:string, balance:number) {


    
    console.log('addUserData :' + userName +"  bal: "+ balance);
  }
}
