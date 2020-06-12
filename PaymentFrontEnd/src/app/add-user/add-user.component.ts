import { Component, OnInit } from '@angular/core';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

 userName:string="dddddddd";
 userBalance:number=100000;

  constructor() { }

  ngOnInit(): void {
  }

  AddUser() {
 console.log("Add User Balance  " + this.userBalance);
 console.log("Add User Name  " + this.userName);

  }
  
}
