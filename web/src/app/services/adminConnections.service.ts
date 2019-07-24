import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminConnectionsService {

  baseUrl = 'http://clickandgoja.com/api/';
  readonly token = localStorage.getItem('token');
  readonly header = new HttpHeaders().set('Authorization', 'Bearer ' + this.token);

constructor(private http: HttpClient) { }

  getOwnerOperator(id) {
    let url = this.baseUrl + 'admin/getSingleOwner/';

    return this.http.get(url + id, {headers: this.header}).pipe(
      map((response: any) => {
        if (response) {
          
          return response;
        }
      })
    );
  }

  getAllDrivers(id) {
    let url = this.baseUrl + 'admin/getAllDrivers/';

    return this.http.get(url + id, {headers: this.header}).pipe(
      map((response: any) => {
        if (response) {
          
          return response;
        }
      })
    );
  }

  getCarById(id){
    let url = this.baseUrl + 'admin/getSingleVehicle/';

    return this.http.get(url + id, {headers: this.header}).pipe(
      map((response: any) => {
        if (response) {
          
          return response;
        }
      })
    );
  }

  getAllVehicles(id){
    let url = this.baseUrl + 'admin/getAllVehicles/' + id;

    return this.http.get(url, {headers: this.header}).pipe(
      map((response: any) => {
        if (response) {
          
          return response;
        }
      })
    );
  }

  updateApprovalStatus(id,data){
    return this.http.post(this.baseUrl+ 'admin/setApprovedStatus/' + id + '/'+ data,null);
  }

}
