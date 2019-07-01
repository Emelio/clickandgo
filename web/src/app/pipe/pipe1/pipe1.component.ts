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
  verification: any; 

  constructor(private communicate: CommunicatorService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.checkUser();
    this.local.fname = localStorage.getItem('fname');
    this.local.lname = localStorage.getItem('lname');
    this.local.email = localStorage.getItem('email');

    
  }

  checkUser() {
    
    this.communicate.getSingleUser().subscribe(next => {
      console.log(next.status);

      if(next.status == 'false') {
        this.verification = false;
      } else if(next.status == 'true') {
        this.verification = true;
      }

    });
  }

  checkStage() {
    this.communicate.checkUserStage().subscribe(next => {

      switch (next.stage) {
        case 'first':
          this.router.navigate(['/pipe/pipe1']);

          break;

        case 'second':


          this.router.navigate(['/pipe/pipe2']);
          break;

          case 'third':


            this.router.navigate(['/pipe/pipe3']);
            break;  

        case 'approved':
          this.router.navigate(['/pipe/pipe3']);
          break;
      
        default:
          break;
      }
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

        this.checkStage();

      }

    });
  }

}
