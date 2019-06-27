import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from '../services/communicator.service';
import { RouterModule, Router } from '@angular/router';
import { AlertifyService } from '../services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  data: any = {};

  constructor(private communicate: CommunicatorService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }


  register() {

    if (this.data.Agree === true) {

      if (this.data.Password == this.data.Password1) {

        localStorage.setItem('fname', this.data.FirstName);
        localStorage.setItem('lname', this.data.LastName );
        localStorage.setItem('email', this.data.Email);

        this.communicate.register(this.data).subscribe(next => {
          if (next.status == 'success') {
            this.alertify.success('Account Created Successfully');
            localStorage.setItem('token', next.token.value.token);
            this.router.navigate(['/pipe/pipe1']);
          } else {
            this.alertify.error('User account already exists');
          }
          console.log(next.status);
        });
      }

    }

  }
}
