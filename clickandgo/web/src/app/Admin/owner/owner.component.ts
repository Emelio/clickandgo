import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommunicatorService } from 'src/app/services/communicator.service';
import { AlertifyService } from 'src/app/services/alertify.service';


@Component({
  selector: 'app-owner',
  templateUrl: './owner.component.html',
  styleUrls: ['./owner.component.css']
})
export class OwnerComponent implements OnInit {

  data: any = {};
  genders: any = [{ title: 'Male', value: 'male' }, { title: 'Female', value: 'female' }];

  constructor(private router: Router, private communicate: CommunicatorService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate['/login'];
  }

  createOwnerOperator() {
    this.communicate.createOwnerOperator(this.data).subscribe(next => {
      console.log(next);

      if (next.status == 'email sent') {
        this.router.navigate(['/dash']);
        this.alertify.success('account created successfully');
      }
    });
  }

}
