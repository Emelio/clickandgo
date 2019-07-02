import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommunicatorService } from '../services/communicator.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  code: any;
  email: any;
  verified: any;
  data: any ={};

  constructor(private route: ActivatedRoute, private communicate: CommunicatorService,private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.code = this.route.snapshot.paramMap.get('code');
    this.email = this.route.snapshot.paramMap.get('email');

    console.log(this.code + this.email);
    this.communicate.verifyUser(this.code, this.email).subscribe(next => {
      console.log(next);

      if (next.status == 'success') {
        this.verified = true;
        this.data.email = next.email;
      }
    });
  }

  resetPassword(){
    if (this.data.Password == this.data.Password1) {
      this.communicate.resetPassword(this.data).subscribe(next =>{
        if(next){
          this.router.navigateByUrl('/login');
        }else{
          this.alertify.error("failed to update password");
        }
        
      });
    }
  }
}
