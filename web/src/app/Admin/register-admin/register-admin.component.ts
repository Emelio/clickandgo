import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from 'src/app/services/communicator.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register-admin',
  templateUrl: './register-admin.component.html',
  styleUrls: ['./register-admin.component.css']
})
export class RegisterAdminComponent implements OnInit {

  data: any = {};
  code: any;
  constructor(private communicate: CommunicatorService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
  }


  register(){
    if (this.data.Password == this.data.Password1) {
      this.communicate.confirmCode(this.code).then(response =>{
        if(response){
          this.communicate.registerAdmin(this.data).subscribe(next => {
            if (next) {
              this.alertify.success('success');
              window.location.href= '/dash';
           } else {
              this.alertify.error('User account already exists');
            }
     });
      }
    });
    }
  }
}
