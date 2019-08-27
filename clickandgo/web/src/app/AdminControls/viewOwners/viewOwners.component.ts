import { Component, OnInit } from '@angular/core';
import { AdminConnectionsService } from 'src/app/services/adminConnections.service';
import { PlatformLocation } from '@angular/common'

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
  listOfCars: any = [];
  

  constructor(private adminServ: AdminConnectionsService, location: PlatformLocation) {

    location.onPopState(() => {
      window.location.href = '/dash';
    });
   }

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

    this.adminServ.getAllVehicles(id).subscribe(next => {
      console.log(next);
      this.listOfCars = next;
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

      this.adminServ.getCarById(element._id).subscribe(next => {
        console.log(next);
        this.selectedVehicle = next; 
      });
 
    });
  }

  updateApprovalStatus(status: any){
    if(confirm( 'Are you sure you want to  ' + this.usersData.firstName + ' status to ' + status + '?')){
      console.log('jer');
      this.adminServ.updateApprovalStatus(this.usersData._id, status).subscribe();
    }else{
      window.location.reload();
    }


  }


}
