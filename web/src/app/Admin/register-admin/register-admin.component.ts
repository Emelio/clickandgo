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
  rcode: any;
  dcode: any;
  admins: any =[];
  selectedAdmin: string;
  constructor(private communicate: CommunicatorService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getAdmins();
  }

  getAdmins(){
    this.communicate.getAdmins().subscribe(response =>{
      this.admins=response;
      this.selectedAdmin = this.admins[0].id;
    });
  }

  deleteAdmin(){
    if(confirm('Are you sure?')){
      this.communicate.confirmCode(this.dcode).then(response =>{
        if(response){
          this.communicate.deleteAdmin(this.selectedAdmin).subscribe(response =>{
            if(response){
              this.getAdmins();
              this.dcode= '';
              this.alertify.success('success');
            }
          });
          
      }
    });
    }
  }
  register(){
    if(confirm('Are you sure you want to add admin')){
      if (this.data.Password == this.data.Password1) {
        this.communicate.confirmCode(this.rcode).then(response =>{
          if(response){
            this.communicate.registerAdmin(this.data).subscribe(next => {
              if (next) {
                this.alertify.success('success');
                this.data= {};
                this.rcode= '';
                this.getAdmins();
             } else {
                this.alertify.error('User account already exists');
              }
       });
        }
      });
      }
    }
  }
}
