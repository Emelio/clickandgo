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
            window.location.href = '/ownerDash';
          break;
      }
    });
  }

  login() {

    
    this.communicate.login(this.data).subscribe(next => {

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

              localStorage.setItem('email', this.data.Email);

              this.checkStage();

            //window.location.href = '/ownerDash';
            break;
        
          default:
            break;
        }

        
      }

    });
  }

}
