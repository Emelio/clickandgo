import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from 'src/app/services/communicator.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pipe3',
  templateUrl: './pipe3.component.html',
  styleUrls: ['./pipe3.component.css']
})
export class Pipe3Component implements OnInit {

  data: any = {};
  genders: any = [{ title: 'Male', value: 'male' }, { title: 'Female', value: 'female' }];

  constructor(private communicate: CommunicatorService, private router: Router) { }

  ngOnInit() {
  }

  addDriverInfo() {
    this.communicate.createDriverData(this.data).subscribe(next => {
      console.log(next);
      window.location.href = '/ownerDash';
    });
  }
}
