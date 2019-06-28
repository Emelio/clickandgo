import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from 'src/app/services/communicator.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-pipe1',
  templateUrl: './pipe1.component.html',
  styleUrls: ['./pipe1.component.css']
})
export class Pipe1Component implements OnInit {

  local: any = {};
  data: any = {};
  genders: any = [{ title: 'Male', value: 'male' }, { title: 'Female', value: 'female' }];

  constructor(private communicate: CommunicatorService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.local.fname = localStorage.getItem('fname');
    this.local.lname = localStorage.getItem('lname');
    this.local.email = localStorage.getItem('email');

    this.checkUser();
  }

  checkUser(){
    this.communicate.getSingleUser().subscribe(next => {
      console.log(next);

    });
  }

  createOwnerOperator() {
    this.data.Stage = 'first';
    this.data.Email = this.local.email;
    this.data.FirstName = this.local.fname;
    this.data.LastName = this.local.lname;
    console.log(this.data);
    this.communicate.updateOwnerOperator(this.data).subscribe(next => {
      
      console.log(next);

      if(next.status == 'updated') {
         this.router.navigate(['/pipe/pipe2']);

      }

    });
  }

}
