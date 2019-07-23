import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommunicatorService } from '../services/communicator.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-dash',
  templateUrl: './dash.component.html',
  styleUrls: ['./dash.component.css']
})
export class DashComponent implements OnInit {

  arrayOfOwners: any = [];
  selectedDriver: any = {};
  diverCount;

  constructor(private router: Router, private communicate: CommunicatorService, private alertify: AlertifyService) { }

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

        if (userArray != undefined) { }
        
      });

    });
  }

  goToUser(id) {
    localStorage.setItem('selectedOwner', id);
    this.router.navigateByUrl('admin/viewOwners');
  }

  getSingleDriver(userID) {
    this.communicate.getSingleDriver(userID).subscribe(next => {
      this.selectedDriver = next;
    });
  }

  deleteOwner(id) {
    if (confirm('Are you sure you want to delete user?')) {
      this.communicate.deleteUser(id).subscribe(next => {
        if (next) {
          //window.location.reload();
        } else {
          this.alertify.error(JSON.stringify(next));
        }
      });
    }
  }

}
