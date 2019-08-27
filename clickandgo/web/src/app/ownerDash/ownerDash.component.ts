import { Component, OnInit } from '@angular/core';
import { CommunicatorService } from '../services/communicator.service';
import { Router } from '@angular/router';
import { UpdateDriver } from '../model/updateDriver';


@Component({
  selector: 'app-ownerDash',
  templateUrl: './ownerDash.component.html',
  styleUrls: ['./ownerDash.component.css']
})
export class OwnerDashComponent implements OnInit {
  approved = false;
  cars: any = [];
  drivers: any = [];
  selectedCar: any = {};
  selectedDriver: any = {};
  selectedCarString = '';
  owner = '';
  data: any = {};
  carId = '';
  driverId = '';
  genders: any = [{ title: 'Male', value: 'male' }, { title: 'Female', value: 'female' }];

  driverData: UpdateDriver = new UpdateDriver();

  constructor(private communicate: CommunicatorService, private router: Router) { }

  ngOnInit() {

  
    this.getVehicleList();
    this.getDriverList();

    console.log(this.selectedDriver);
  }
  
  findSelectedCar(id) {
    this.carId = id;
    for (const car of this.cars) {
      
      if (car._id == id) {
        this.selectedCar = car;
      }
    }
  }
  
  // check user stage


  getVehicleList() {
    this.communicate.carList().subscribe(next => {

      this.cars = next;
    });
  }

  getDriverList() {
    this.communicate.getDriverListData().subscribe(next => {
      this.drivers = next;

    });
  }

  logout() {
    localStorage.removeItem('token');
    window.location.href = '/login';
  }

  updateCarDetails(event: any) {
    this.data.vehicleId = this.selectedCar._id;
    this.data.OwnerId = this.selectedCar.ownerId;
    this.communicate.updateCar(this.data).subscribe(next => {

      window.location.reload();
    });
  }

  removeCar(id) {
    this.communicate.removeCar(id).subscribe(next => {

      for (let index = 0; index < this.cars.length; index++) {
        if (this.cars[index]._id == id) {
          this.cars.splice(index, 1);

        }
        
      }
    });
  }

  removeDriver(id) {
    this.communicate.removeDriverData(id).subscribe(next => {

      for (let index = 0; index < this.drivers.length; index++) {
        if (this.drivers[index]._id == id) {
          this.drivers.splice(index, 1);
        }
      }
    });
  }

  assignDriverEvent(event) {
    this.selectedCarString = event.target.value; 
  }

  assignDriver(id, setting) {
    if (setting == 'first') {
      this.driverId = id;
    } else {
      this.communicate.assignCarToDriverData(this.driverId, this.selectedCarString).subscribe(next => {
        if (next.isAcknowledged == true) {
          window.location.reload();
        }
      });
    }
  }

  getCar(driverId) {
    let carIdentifier;
    
    this.drivers.forEach(element => {
      if (element._id == driverId) {
        carIdentifier = element.vehicleID;
        this.selectedDriver = element;
        
      }
    });
    
    
    this.cars.forEach(element => {

      if (element._id == carIdentifier) {
        this.selectedCar = element;
      }
    });

  }

  editDriver(id){
    this.communicate.getSingleDriver(id).subscribe(next => {
      this.selectedDriver = next;
      this.driverData = next;
      localStorage.setItem('drvID',next._id);

      console.log(this.selectedDriver);
    })
  }

  updateDriver(){
    const driverID= localStorage.getItem('drvID');
    
    this.driverData.driverID = driverID;

    console.log(this.driverData);

    this.communicate.updateDriver(this.driverData).subscribe(next =>{
      localStorage.removeItem('drvID');
      window.location.reload();
    });
  }


}
