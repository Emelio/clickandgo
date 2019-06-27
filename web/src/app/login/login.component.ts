import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from '../services/communicator.service';
import { AlertifyService } from '../services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  data: any = {};

  constructor(private communicate: CommunicatorService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {

    
    this.communicate.login(this.data).subscribe(next => {
      console.log("hi");
      console.log(next);

      if (next.error) {
        this.alertify.error('username or password is incorrect');
      }

      if (next.token) {

        switch (next.type) {
          case 'admin':
            
            window.location.href = '/dash';
            break;

          case 'owner':
            window.location.href = '/ownerDash';
            break;
        
          default:
            break;
        }

        
      }

    });
  }

}
