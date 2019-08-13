import { Injectable } from '@angular/core';
import {HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType} from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  private connection: HubConnection;

  public startConnection = () => {
    this.connection = new HubConnectionBuilder()
     .withUrl('http://localhost:5000/message')
     .build();

    this.connection
      .start()
      .then(() => {
        this.Reievemessage();
        console.log('Hub connection started');
        this.sendmessage();
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
      });
  }

  public sendmessage = () => {
    this.connection.invoke('SendMessage', 'collin', 'testmessage').catch( (err) => {
            return console.error(err.toString());
        });
  }

  public Reievemessage = () => {
    this.connection.on('ReceiveMessage', (data) => {
               // this.data = data;
               console.log(data);
             });

  }
  constructor() { }
}
