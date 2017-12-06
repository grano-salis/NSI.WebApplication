import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client'

@Component({
  selector: 'app-conversations',
  templateUrl: './conversations.component.html',
  styleUrls: ['./conversations.component.scss']
})
export class ConversationsComponent implements OnInit {

  private _hubConnection: HubConnection;
  public async: any;
  public message: string = '';
  
  public messages: string[] = [];

  constructor() {
  }

  public sendMessage(): void {
      const data = `Sent: ${this.message}`;

      this._hubConnection.invoke('Send', data);
      //this.messages.push(data);
  }

  ngOnInit() {
      this._hubConnection = new HubConnection('http://localhost:59738/chat');

      this._hubConnection.on('Send', (data: any) => {
          const received =  data;
          this.messages.push(received);
      });

      this._hubConnection.start()
      .then(() => {
          console.log('Hub connection started')
      })
      .catch(err => {
          console.log('Error while establishing connection')
      });
    }
}
