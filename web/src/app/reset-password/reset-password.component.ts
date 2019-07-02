import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from '../services/communicator.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  data: any ={};
  constructor(private communicate: CommunicatorService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  reset(){
    this.communicate.forgotpassword(this.data).subscribe(next =>{
      if(next){
        this.alertify.message('Check email for reset link');
      }else{
        this.alertify.error('failed to verify email try again');
      }
    })
  }
}
