import { Component, OnInit } from '@angular/core';
import { HubService } from './services/hub.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'web';

  constructor(public signalRService: HubService) { }
 
   ngOnInit() {
    this.signalRService.startConnection();
  }
}

