import { Component, OnInit } from '@angular/core';
import { AdminConnectionsService } from 'src/app/services/adminConnections.service';

@Component({
  selector: 'app-viewOwners',
  templateUrl: './viewOwners.component.html',
  styleUrls: ['./viewOwners.component.css']
})
export class ViewOwnersComponent implements OnInit {

  usersData: any = {};
  drivers: any = [];
  selectedDriver: any = {}
  selectedDriverage: number;
  selectedVehicle: any = {};

  constructor(private adminServ: AdminConnectionsService, ) { }

  ngOnInit() {
    this.getOwnerData();
  }

  getOwnerData() {
    let id = localStorage.getItem('selectedOwner'); 
    this.adminServ.getOwnerOperator(id).subscribe(next => {
      console.log(next);
      this.usersData = next;
    });

    this.adminServ.getAllDrivers(id).subscribe(next => {
      this.drivers = next;
      console.log(this.drivers);
    });
  }

  showUsers(id) {
    this.drivers.forEach(element => {
      console.log(element);
      this.selectedDriver = element;

      const today = new Date();
      const dob = new Date(element.dob);
      let age = today.getFullYear() - dob.getFullYear();
      this.selectedDriverage = age;

      this.adminServ.getCarById(element.vehicleID).subscribe(next => {
        console.log(next);
        this.selectedVehicle = next; 
      });
 
    });
  }


}
