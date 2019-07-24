import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ThrowStmt } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class CommunicatorService {

  baseUrl = 'http://clickandgoja.com/api/';
  secondBase = 'http://localhost:5000/api/';
  registerUrl =   this.baseUrl + 'users/register';
  loginUrl =      this.baseUrl + 'users/login/';
  createUrl =     this.baseUrl + 'users/adminCreateOwner';
  updateUserUrl = this.baseUrl + 'users/createOwner';
  getUserUrl =    this.baseUrl + 'users/getUser';
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
  updateDriverDetails = this.baseUrl + 'owner/updateDriver';
  verifyUserFromEmail = this.baseUrl + 'users/verify/';
  resetpassword= this.baseUrl + 'users/resetpassword/';
  forgetpassword =this.baseUrl + 'users/forgotpassword/'
  deleteOwner =this.baseUrl +'admin/removeDriver/';
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

verifyUser(code, email) {
  return this.http.get(this.verifyUserFromEmail + code + "/" + email, {headers: this.header}).pipe(
    map((response: any) => {
      if (response) {
        
        return response;
      }
    })
  );
}

getSingleUser() {

  return this.http.get(this.getUserUrl, {headers: this.header}).pipe(
    map((response: any) => {
      console.log(response);
      if (response) {
        console.log(response);

        
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
          console.log(response);
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
  const token = localStorage.getItem('token');
  const header = new HttpHeaders().set('Authorization', 'Bearer ' + token);

  return this.http.get(this.checkStageUrl, {headers: header}).pipe(
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

updateDriver(data: any) {
  return this.http.post(this.updateDriverDetails, data, {headers: this.header});
}

resetPassword(data:any){
  return this.http.post(this.resetpassword,data);
}

forgotpassword(data:any){
  return this.http.post(this.forgetpassword + data.email,null);
}

deleteUser(id){
  return this.http.post(this.deleteOwner + id,null);
}

registerAdmin(data: any){
  return this.http.post(this.secondBase + 'admin/registerAdmin' , data );
}
}
