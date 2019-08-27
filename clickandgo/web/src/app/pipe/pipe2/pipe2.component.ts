import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from 'src/app/services/communicator.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pipe2',
  templateUrl: './pipe2.component.html',
  styleUrls: ['./pipe2.component.css']
})
export class Pipe2Component implements OnInit {

  data: any = {};

  constructor(private communicate: CommunicatorService, private router: Router) { }

  ngOnInit() {
  }

  updateCarDetails() {
    this.communicate.CreateOwnerCar(this.data).subscribe(next => {

      window.location.href = '/pipe/pipe3';
    });
  }

}
