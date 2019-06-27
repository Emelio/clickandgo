import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommunicatorService {

  baseUrl = 'http://clickandgoja.com/api/';
  registerUrl =   this.baseUrl + 'users/register';
  loginUrl =      this.baseUrl + 'users/login/';
  createUrl =     this.baseUrl + 'users/adminCreateOwner';
  updateUserUrl = this.baseUrl + 'users/createOwner';
  checkStageUrl = this.baseUrl + 'owner/checkStage';
  addcar =        this.baseUrl + 'owner/addCar';
  updatecar =     this.baseUrl + 'owner/updateCar';
  getCarList =    this.baseUrl + 'owner/getunits';
  removeCarData =  this.baseUrl + 'owner/removeVehicle/';
  createDriver =  this.baseUrl + 'owner/addDriver';
  getDriverList = this.baseUrl + 'owner/getDriverList';
  removeDriver = this.baseUrl + 'owner/removeDriver/';
  assignCarToDriver = this.baseUrl + 'owner/assignDriver/';
  getAllOwnersData = this.baseUrl + 'admin/getAllOwners';
  getSingleDriverData = this.baseUrl + 'admin/getSingleDriver/';

  readonly token = localStorage.getItem('token');
  readonly header = new HttpHeaders().set('Authorization', 'Bearer ' + this.token);


constructor(private http:HttpClient) { }

getSingleDriver(id) {
  return this.http.get(this.getSingleDriverData + id, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        
        return response;
      }
    })
  );
}

getAllOwner() {
  return this.http.get(this.getAllOwnersData).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
  ); 
}

assignCarToDriverData(driverId, carId) {
  return this.http.get(this.assignCarToDriver + driverId + '/' + carId, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
);
}

removeDriverData(id) {
  return this.http.get(this.removeDriver + id, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
);
}

getDriverListData() {
  return this.http.get(this.getDriverList, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
);
}

createDriverData(data: any) {
  return this.http.post(this.createDriver, data, {headers: this.header}).pipe(
      map((response: any) => {
        if (response) {
          return response;
        }
      })
  );
}

register(data: any) {
  return this.http.post(this.registerUrl, data).pipe(
      map((response: any) => {
        if (response) {
          return response;
        }
      })
  );
}

login(data: any) {
  
  return this.http.get(this.loginUrl + data.Email + '/' + data.Password).pipe(
    map((response: any) => {
      if (response) {
        console.log(response);
        localStorage.setItem('token', response.token);
        return response;
      }
    })
  );
}

createOwnerOperator(data: any) {
  return this.http.post(this.createUrl, data, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
  );
}

updateOwnerOperator(data: any) {
  return this.http.post(this.updateUserUrl, data, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
  );
}

CreateOwnerCar(data: any) {
  return this.http.post(this.addcar, data, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
  );
}

checkUserStage() {
  return this.http.get(this.checkStageUrl, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        return response;
      }
    })
  );
}

carList() {
  return this.http.get(this.getCarList, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        console.log(response);
        return response;
        
      }
    })
  );
}

updateCar(data: any) {
  return this.http.post(this.updatecar, data,{headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        console.log(response);
        return response;
      }
    })
  );
}

removeCar(id) {
  return this.http.get(this.removeCarData + id, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        console.log(response);
        return response;
      }
    })
  );
}

}
