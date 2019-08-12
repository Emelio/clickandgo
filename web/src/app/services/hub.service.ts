import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  private hubConnection: signalR.HubConnection;
 
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:5001/api/message')
                            .build();
 
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
 
  public addTransferChartDataListener = () => {
    this.hubConnection.on('RecieveMessage', (data) => {
      //this.data = data;
      console.log(data);
    });
  }

  public sendmessage = () =>{
    this.hubConnection.invoke('SendMessage', 'collin', 'testmessage').catch( (err) =>{
      return console.error(err.toString());
  });
  }
  constructor() { }
}
