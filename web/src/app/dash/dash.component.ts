import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommunicatorService } from '../services/communicator.service';

@Component({
  selector: 'app-dash',
  templateUrl: './dash.component.html',
  styleUrls: ['./dash.component.css']
})
export class DashComponent implements OnInit {

  arrayOfOwners: any = [];
  selectedDriver: any = {};
  diverCount;

  constructor(private router: Router, private communicate: CommunicatorService) { }

  ngOnInit() {
    this.getOwners();
  }

  

  getOwners() {
    this.communicate.getAllOwner().subscribe(next => {
      this.arrayOfOwners = next;
      // count drivers // get all drivers from database
      console.log(this.arrayOfOwners);
      this.arrayOfOwners.forEach(owner => {
        let userArray: any = [];

        userArray = this.getSingleDriver(owner._id);

        console.log(userArray);

        if (userArray != undefined) {
        }
        
      });

    });
  }

  goToUser(id){
    localStorage.setItem('selectedOwner', id);
    this.router.navigateByUrl('admin/viewOwners');
  }

  getSingleDriver(userID) {
    this.communicate.getSingleDriver(userID).subscribe(next => {
      this.selectedDriver = next;
    });
  }

}
