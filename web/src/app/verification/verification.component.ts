import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommunicatorService } from '../services/communicator.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent implements OnInit {

  code: any;
  email: any;

  constructor(private route: ActivatedRoute, private communicate: CommunicatorService) { }

  ngOnInit() {
    this.code = this.route.snapshot.paramMap.get('code');
    this.email = this.route.snapshot.paramMap.get('email');

    this.communicate.verifyUser(this.code, this.email).subscribe(next => {
      console.log(next);
    });

    console.log(this.code);
  }
  



}
